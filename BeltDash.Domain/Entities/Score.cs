using BeltDash.Domain.Entities.Common;

namespace BeltDash.Domain.Entities
{
    /// <summary>
    /// Entity that represents a score achieved by a user in the game.
    /// Records players' achievements to implement features such as leaderboards.
    /// </summary>
    public class Score : BaseEntity
    {
        /// <summary>
        /// Identifier of the user associated with this score.
        /// Foreign key for the relationship with the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Reference to the user who achieved this score.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Points earned by the user in a game session.
        /// </summary>
        public int Points { get; set; }
    }
}

