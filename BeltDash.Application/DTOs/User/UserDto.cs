﻿using BeltDash.Domain.Enums;

namespace BeltDash.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public UserStatus Status { get; set; }

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
