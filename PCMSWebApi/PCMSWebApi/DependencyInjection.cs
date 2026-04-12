using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PCMS.Application;
using PCMS.Domain;
using PCMS.Infrastructure;

namespace PCMS.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
            services.AddDomainDI();
            services.AddInfrastructureDI();
            return services;
        }
    }
}
