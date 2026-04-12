using PCMS.Domain.Entities;

namespace PCMS.Infrastructure.Caching
{
    public class SearchCache
    {
        private readonly Dictionary<string, IEnumerable<Product>> _cache = new();

        public bool TryGet(string key, out IEnumerable<Product>? result)
            => _cache.TryGetValue(key, out result);

        public void Set(string key, IEnumerable<Product> data)
            => _cache[key] = data;
    }
}
