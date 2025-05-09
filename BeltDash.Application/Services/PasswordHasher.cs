using BeltDash.Application.Interfaces;

namespace BeltDash.Application.Services
{
    /// <summary>
    /// Implementation of the password hashing service using BCrypt.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Generates a secure hash of the provided password.
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Password hash</returns>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies if a password matches a stored hash.
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hash">Stored hash</param>
        /// <returns>True if the password is correct</returns>
        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
