namespace BeltDash.Application.Interfaces
{
    /// <summary>
    /// Interface for password hashing services.
    /// Defines methods to generate hashes and verify passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Generates a secure hash of the provided password.
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Password hash</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies if a password matches a stored hash.
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hash">Stored hash</param>
        /// <returns>True if the password is correct</returns>
        bool VerifyPassword(string password, string hash);
    }
}
