using PCMS.Domain.Entities;

namespace PCMS.Application.Common.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<Product> search(string term);
    }
}
