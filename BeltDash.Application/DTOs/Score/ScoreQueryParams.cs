namespace BeltDash.Application.DTOs.Score
{
    /// <summary>
    /// Query parameters to filter and sort scores.
    /// Defines pagination and sorting options for score queries.
    /// </summary>
    public class ScoreQueryParams
    {
        /// <summary>
        /// Maximum allowed page size.
        /// </summary>
        private const int MaxPageSize = 50;

        /// <summary>
        /// Default page size.
        /// </summary>
        private int _pageSize = 10;

        /// <summary>
        /// Requested page number (default is 1).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size with validation to not exceed the maximum allowed.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        /// <summary>
        /// Field by which the results will be sorted (default is points).
        /// </summary>
        public string? SortBy { get; set; } = "points";

        /// <summary>
        /// Indicates if the sort order is ascending (default is false, sorting from highest to lowest).
        /// </summary>
        public bool Ascending { get; set; } = false;
    }
}
