using Microsoft.Extensions.DependencyInjection;
using PCMS.Application.Common.Services;

namespace PCMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddSingleton<CategoryTreeBuilder>();
            return services;
        }
    }
}
