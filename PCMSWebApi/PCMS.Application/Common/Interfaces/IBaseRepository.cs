using PCMS.Domain.Interfaces;

namespace PCMS.Application.Common.Interfaces
{
    public interface IBaseRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
