namespace BeltDash.Domain.Enums
{
    /// <summary>
    /// Enumeration that defines the possible statuses of a user in the system.
    /// Allows managing permissions and access to the application.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// Active user with full access to the system.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Suspended user without access to the system.
        /// </summary>
        Banned = 1
    }
}

