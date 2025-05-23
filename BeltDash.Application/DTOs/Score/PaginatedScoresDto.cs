namespace BeltDash.Application.DTOs.Score
{
    /// <summary>
    /// DTO to represent a paginated list of scores.
    /// Facilitates the implementation of pagination in score queries.
    /// </summary>
    public class PaginatedScoresDto
    {
        /// <summary>
        /// Collection of scores for the current page.
        /// </summary>
        public IEnumerable<ScoreDto> Scores { get; set; } = new List<ScoreDto>();

        /// <summary>
        /// Total number of score records in the database.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Selected page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Calculation of the total number of pages based on the total records and page size.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Indicates if there is a previous page before the current one.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Indicates if there is a next page after the current one.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;
    }
}
