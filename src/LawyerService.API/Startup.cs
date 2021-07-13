using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using LawyerService.BL;
using LawyerService.BL.Interfaces;
using LawyerService.Bootstrapper.Extensions;
using LawyerService.DataAccess;
using LawyerService.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.Extensions.Hosting;

namespace LawyerService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiControllersWithValidation(new List<Assembly> {
                typeof(BL.Validators.BaseValidator<>).Assembly
            });

            services.AddDbContext<LawyerDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddScoped<IUow, Uow>();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddScoped<ILocalisationManager, LocalisationManager>();

            services.AddScoped<ILawyerManager, LawyerManager>();
            services.AddAutoMapperProfiles();

            services.AddLogging(loggingBuilder =>
            {
                _ = loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddNLog(Configuration);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lawyer", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCacheManager memoryCacheManager)
        {
            memoryCacheManager.Put("LOCALIZATION", typeof(Resources.Resources).Assembly);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseMiddleware<ExceptionResponseMiddleware>();
            }
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json",   "Lawyer");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "defaultArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
