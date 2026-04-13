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

            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();
            services.AddSingleton<ProductSearchEngine<Product>>();
            services.AddSingleton<SearchCache>();

            return services;
        }
    }
}
