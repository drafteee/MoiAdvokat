using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace LawyerService.Bootstrapper.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Bootstrapper).Assembly);
        }
    }
}
