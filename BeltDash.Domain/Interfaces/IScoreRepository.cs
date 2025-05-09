using BeltDash.Domain.Entities;
using BeltDash.Domain.Interfaces.Common;

namespace BeltDash.Domain.Interfaces
{
    public interface IScoreRepository : IRepository<Score>
    {
        Task<IEnumerable<Score>> GetScoresByUserIdAsync(int userId);
        Task<IEnumerable<Score>> GetTopScoresAsync(int count);
        Task<IEnumerable<Score>> GetPaginatedScoresAsync(int pageNumber, int pageSize, string? sortBy = null, bool ascending = false);

    }
}
