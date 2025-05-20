using BeltDash.Domain.Entities.Common;
using System.Linq.Expressions;

namespace BeltDash.Domain.Interfaces.Common
{
    /// <summary>
    /// Generic interface for the Repository pattern.
    /// Defines basic CRUD operations for any domain entity.
    /// </summary>
    /// <typeparam name="T">Type of entity that inherits from BaseEntity</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves all entities that satisfy a specific condition.
        /// </summary>
        /// <param name="predicate">Lambda expression defining the filter condition</param>
        /// <returns>Collection of filtered entities</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity to find</param>
        /// <returns>The found entity or null if it does not exist</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Searches for an entity that satisfies a specific condition.
        /// </summary>
        /// <param name="predicate">Lambda expression defining the search condition</param>
        /// <returns>The first entity that meets the condition or null if none exists</returns>
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">Entity with the changes to update</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity);
    }
}
