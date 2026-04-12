
using PCMS.Application;
using PCMS.Domain;
using PCMS.Infrastructure;

namespace PCMS.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
            services.AddDomainDI();
            services.AddInfrastructureDI();
            return services;
        }
    }
}
