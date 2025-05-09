using System.Linq.Expressions;
using BeltDash.Domain.Entities.Common;

namespace BeltDash.Domain.Interfaces.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
