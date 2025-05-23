namespace BeltDash.Application.DTOs.Score
{
    /// <summary>
    /// DTO to represent a score in the game.
    /// Contains basic information about a score achieved by a user.
    /// </summary>
    public class ScoreDto
    {
        /// <summary>
        /// Unique identifier of the score.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the user who achieved the score.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Username of the player who achieved the score.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Points earned in the game session.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Date and time when the score was recorded.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
