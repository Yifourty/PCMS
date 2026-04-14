using PCMS.Application.Common.Interfaces;
using PCMS.Domain.Interfaces;

namespace PCMS.Infrastructure.Data.Repositories
{
    public class InMemoryRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        protected readonly Dictionary<Guid, T> _store = new();

        public virtual IEnumerable<T> GetAll()
        {
            return _store.Values;
        }

        public virtual T? GetById(Guid id)
        {
            return _store.TryGetValue(id, out var entity) ? entity : null;
        }

        public virtual void Add(T entity)
        {
            _store[entity.Id] = entity;
        }

        public virtual void Update(T entity)
        {
            if (_store.ContainsKey(entity.Id))
            {
                _store[entity.Id] = entity;
            }
        }

        public virtual void Delete(Guid id)
        {
            _store.Remove(id);
        }
    }

}
