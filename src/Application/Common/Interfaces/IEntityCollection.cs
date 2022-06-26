using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IEntityCollection<T> where T : BaseEntity
    {
        Task<Guid> Add(T entity);
        Task Delete(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
        Task<IEnumerable<T>> Get();
        Task<T> Get(Guid id);
    }
}
