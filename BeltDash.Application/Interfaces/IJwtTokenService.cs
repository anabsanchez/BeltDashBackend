using BeltDash.Domain.Entities;

namespace BeltDash.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, string roleName);
    }
}
