using Microsoft.Extensions.DependencyInjection;
using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;
using PCMS.Infrastructure.Caching;
using PCMS.Infrastructure.Data;
using PCMS.Infrastructure.Search;

namespace PCMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {

            services.AddSingleton<IBaseRepository<Product>, InMemoryRepository<Product>>();
            services.AddSingleton<IBaseRepository<Category>, InMemoryRepository<Category>>();
            services.AddSingleton<ProductSearchEngine<Product>>();
            services.AddSingleton<SearchCache>();

            return services;
        }
    }
}
