using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;

namespace PCMS.Infrastructure.Data
{
    public class InMemoryProductRepository : InMemoryRepository<Product>, IProductRepository
    {

        public IEnumerable<Product> search(string term)
        {
            return _store.Values
                .Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase));
        }
    }
}
