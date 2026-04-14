using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Entities;
using PCMS.Domain.Interfaces;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
