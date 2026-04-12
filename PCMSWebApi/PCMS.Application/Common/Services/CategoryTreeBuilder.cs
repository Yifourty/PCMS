using PCMS.Domain.Entities;

namespace PCMS.Application.Common.Services
{
    public class CategoryTreeBuilder
    {
        public List<Category> BuildTree(IEnumerable<Category> categories)
        {
            var lookup = categories.ToDictionary(c => c.Id);

            foreach (var category in categories)
            {
                if (category.ParentCategoryId is Guid parentId &&
                    lookup.ContainsKey(parentId))
                {
                    lookup[parentId].Children.Add(category);
                }
            }

            return categories.Where(c => c.ParentCategoryId == null).ToList();
        }
    }
}
