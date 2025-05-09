namespace BeltDash.Application.DTOs.Score
{
    public class ScoreDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Points { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
