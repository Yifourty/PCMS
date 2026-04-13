using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;

namespace PCMS.Infrastructure.Data
{
    public class InMemoryCategoryRepository : InMemoryRepository<Category>, ICategoryRepository
    {

    }
}
