using PCMS.Domain.Entities;

namespace PCMS.Application.Common.Extensions
{
    public static class ProductExtensions
    {
        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> products, Guid? categoryId)
        {
            if (categoryId == null) return products;
            return products.Where(p => p.CategoryId == categoryId);
        }

        public static IEnumerable<Product> SearchByName(
            this IEnumerable<Product> products,
            string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return products;

            return products.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
    }
}
