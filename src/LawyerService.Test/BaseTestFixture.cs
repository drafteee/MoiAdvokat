using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LawyerService.BL;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Validators;
using LawyerService.Bootstrapper.Extensions;
using LawyerService.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LawyerService.Test
{
    public class BaseTestFixture : IDisposable
    {
        public readonly IConfiguration Configuration; 
        public IServiceScopeFactory ServiceScopeFactory { get; private set; }
        public BaseTestFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        private IServiceScopeFactory BuildScopeFactory(Action<ServiceCollection> servicesMock)
        {
            var services = new ServiceCollection();

            services.AddSingleton(Configuration);
            services.AddHttpContextAccessor();
            services.AddAutoMapperProfiles();
            services.AddValidatorsFromAssembly(typeof(BaseValidator<>).Assembly);

            services.AddDbContext<LawyerDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);


            services.AddScoped<IUow, Uow>();
            services.AddScoped<IAppContext, AppContextMock>();
            services.AddScoped<ILocalizationManager, LocalizationManager>();
            services.AddSingleton<IMemoryCacheManager, MemoryCacheManager>();
            services.AddScoped<ILawyerManager, LawyerManager>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Debug);
            });

            servicesMock?.Invoke(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        public void Dispose()
        {
        }

        public IServiceScopeFactory SetupBaseServiceScopeFactory(Action<IServiceCollection> servicesMock = null)
        {
            return ServiceScopeFactory = BuildScopeFactory(servicesMock);
        }

        public IServiceScopeFactory GetNewServiceScopeFactory(Action<IServiceCollection> servicesMock = null)
        {
            return BuildScopeFactory(servicesMock);
        }

        // Useful for GC/leaks testing
        public T CallInItsOwnScope<T>(Func<T> getter)
        {
            return getter();
        }
    }
}
