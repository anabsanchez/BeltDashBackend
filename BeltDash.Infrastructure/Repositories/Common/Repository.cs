using BeltDash.Domain.Entities.Common;
using BeltDash.Domain.Interfaces.Common;
using BeltDash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeltDash.Infrastructure.Repositories.Common
{
    /// <summary>
    /// Generic implementation of the Repository pattern.
    /// Provides basic CRUD method implementations defined in the IRepository interface.
    /// Uses Entity Framework Core for data access.
    /// </summary>
    /// <typeparam name="T">Entity type inheriting from BaseEntity</typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Database context for accessing Entity Framework.
        /// </summary>
        protected readonly BeltDashDbContext _context;

        /// <summary>
        /// DbSet specific to the entity type, providing access to EF operations.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor initializing the repository with a database context.
        /// </summary>
        /// <param name="context">Application database context</param>
        public Repository(BeltDashDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities of type T.
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities matching a specific condition.
        /// </summary>
        /// <param name="predicate">Lambda expression defining the filter condition</param>
        /// <returns>Collection of filtered entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity to find</param>
        /// <returns>The found entity or null if it does not exist</returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Finds an entity matching a specific condition.
        /// </summary>
        /// <param name="predicate">Lambda expression defining the search condition</param>
        /// <returns>The first entity matching the condition or null if none found</returns>
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">Entity with changes to update</param>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
