using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LawyerService.Test.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void SwapService<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory)
        {
            foreach (var serviceDescriptor in services.Where(x => x.ServiceType == typeof(TService)).ToList())
            {
                services.Remove(serviceDescriptor);
            }

            services.AddTransient(typeof(TService), (x) => factory(x));
        }
    }
}
