using FluentValidation.AspNetCore;
using LawyerService.DataAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LawyerService.API
{
    public static class StartupConfig
    {
        public static IServiceCollection AddApiControllersWithValidation(this IServiceCollection services, List<Assembly> validatorAssemblies = null)
        {
            services.Configure<IISServerOptions>(options => options.AutomaticAuthentication = false);
            services.AddControllers(x => x.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>())
                .AddNewtonsoftJson(o => o.UseMemberCasing())
                .AddFluentValidation(x =>
                {
                    if (validatorAssemblies != null)
                    {
                        validatorAssemblies.ForEach(assembly => x.RegisterValidatorsFromAssembly(assembly));
                    }

                    x.AutomaticValidationEnabled = false;
                });

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            return services;
        }

        public static IServiceCollection AddDbContextWithIniFile<T>(
          this IServiceCollection services, string filePath, string connectionMetaString) where T : DbContext
        {
            services.AddDbContext<T>(
                options => options.UseSqlServer(connectionMetaString), ServiceLifetime.Scoped);
            return services;
        }

    }
}
