﻿namespace BeltDash.Application.DTOs.Score
{
    public class ScoreQueryParams
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SortBy { get; set; } = "points";

        public bool Ascending { get; set; } = false;
    }
}
