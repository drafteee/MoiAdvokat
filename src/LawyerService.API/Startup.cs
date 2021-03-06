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
using LawyerService.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LawyerService.BL.Helpers;
using Microsoft.AspNetCore.DataProtection;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Addresses;
using LawyerService.BL.Addresses;
using LawyerService.BL.Interfaces.Transactions;
using LawyerService.BL.Transactions;
using LawyerService.BL.Orders;
using LawyerService.BL.Interfaces.Orders;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;

using LawyerService.BL.Interfaces.Reports;
using LawyerService.BL.Reports;
using LawyerService.API.Middleware;
using LawyerService.BL.Interfaces.Lawyers;
using LawyerService.BL.Lawyers;
using LawyerService.BL.Interfaces.Files;
using LawyerService.BL.Files;

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

            //services.AddDbContext<LawyerDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddDbContext<LawyerDbContext>(opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("LawyerService.API")).EnableSensitiveDataLogging(); //���� � output ������ �����
            });

            services.AddScoped<IUow, Uow>();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddScoped<ILocalizationManager, LocalizationManager>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddScoped<IChatManager, ChatManager>();

            services.AddScoped<IUserManager, BL.Account.UserManager>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped<IReportManager, ReportManager>();
            services.AddScoped<IStatisticManager, StatisticManager>();

            services.AddScoped<IFileManager, FileManager>();

            #region Lawyers managers

            services.AddScoped<ILawyerManager, LawyerManager>();
            services.AddScoped<ISpecializationManager, SpecializationManager>();

            #endregion

            #region Address managers

            services.AddScoped<IAddressManager, AddressManager>();
            services.AddScoped<IAdministrativeTerritoryManager, AdministrativeTerritoryManager>();
            services.AddScoped<IAdministrativeTerritoryTypeManager, AdministrativeTerritoryTypeManager>();
            services.AddScoped<IUserConnectionManager, UserConnectionManager>();
            services.AddScoped<ICountryManager, CountryManager>();

            #endregion

            #region Orders managers

            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IOrderStatusManager, OrderStatusManager>();
            services.AddScoped<IOrderResponseManager, OrderResponseManager>();

            #endregion

            services.AddAutoMapperProfiles();
            services.AddHttpContextAccessor();
            services.AddDataProtection().SetDefaultKeyLifetime(TimeSpan.FromDays(365));

            var builder = services.AddIdentityCore<User>();

            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddRoles<Role>();
            identityBuilder.AddEntityFrameworkStores<LawyerDbContext>()
                            .AddDefaultTokenProviders();

            identityBuilder.AddSignInManager<SignInManager<User>>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Tokens")["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RegisterByUser", policy =>
                    policy.Requirements.Add(new RegisterByUserRequirement()));
            });
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(10);
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
            });
            services.AddScoped<IAuthorizationHandler, RegisterByUserHandler>();
            services.AddScoped<JwtGenerator>();
            services.AddScoped<PasswordHasher<User>>();
            
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
            services.AddMvc(options => options.EnableEndpointRouting = false);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCacheManager memoryCacheManager)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalR>("/signalR", options =>
                {
                    options.Transports = HttpTransportType.WebSockets;
                });
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "defaultArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc();
            app.UseFastReport();
        }


    }
}
