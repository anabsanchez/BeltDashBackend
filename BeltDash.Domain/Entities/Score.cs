using BeltDash.Domain.Entities.Common;

namespace BeltDash.Domain.Entities
{
    public class Score : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int Points { get; set; }

    }
}
