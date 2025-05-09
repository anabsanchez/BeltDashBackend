using BeltDash.Domain.Entities.Common;

namespace BeltDash.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set;} = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
 