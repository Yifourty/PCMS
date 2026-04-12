using PCMS.Application.Common.Interfaces;

namespace PCMS.Infrastructure.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        protected readonly Dictionary<Guid, T> _store = new Dictionary<Guid, T>();
        public virtual IEnumerable<T> GetAll()
        {
            return _store.Values;
        }
        public void Add(T entity)
        {
            var id = (Guid)entity.GetType().GetProperty("Id")?.GetValue(entity)!;
            _store[id] = entity;
        }

        public void Delete(Guid id)
        {
            _store.Remove(id);
        }

        public virtual T? GetById(Guid id) => _store.TryGetValue(id, out var entity) ? entity : null;
        
        public void Update(T entity)
        {
            var id = (Guid)entity.GetType().GetProperty("Id")?.GetValue(entity)!;
            if (_store.ContainsKey(id))
            {
                _store[id] = entity;
            }
        }
    }
}
