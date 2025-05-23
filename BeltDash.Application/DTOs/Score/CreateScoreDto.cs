namespace BeltDash.Application.DTOs.Score
{
    /// <summary>
    /// DTO to create a new score in the system.
    /// Contains the minimum information required to record a game result.
    /// </summary>
    public class CreateScoreDto
    {
        /// <summary>
        /// Points earned by the player in the game session.
        /// </summary>
        public int Points { get; set; }
    }
}
