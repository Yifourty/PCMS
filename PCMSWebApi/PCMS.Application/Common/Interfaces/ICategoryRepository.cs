using PCMS.Domain.Entities;

namespace PCMS.Application.Common.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        //IEnumerable<Category> search(string term);
    }
}
