namespace BeltDash.Domain.Exceptions
{
    /// <summary>
    /// Custom exception for domain-specific errors.
    /// Allows differentiation between business logic errors and other types of exceptions.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DomainException() : base() { }

        /// <summary>
        /// Constructor that receives a descriptive error message.
        /// </summary>
        /// <param name="message">Message explaining the reason for the exception</param>
        public DomainException(string message) : base(message) { }

        /// <summary>
        /// Constructor that receives a descriptive message and an inner exception.
        /// Useful for encapsulating low-level exceptions with domain-specific context.
        /// </summary>
        /// <param name="message">Message explaining the reason for the exception</param>
        /// <param name="innerException">Original exception that caused this error</param>
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}

