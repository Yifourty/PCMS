using Microsoft.Extensions.DependencyInjection;
using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;
using PCMS.Infrastructure.Caching;
using PCMS.Infrastructure.Data.Repositories;
using PCMS.Infrastructure.Search;

namespace PCMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {

            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
            services.AddSingleton<ProductSearchEngine<Product>>();
            services.AddSingleton<SearchCache>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));


            return services;
        }
    }
}
