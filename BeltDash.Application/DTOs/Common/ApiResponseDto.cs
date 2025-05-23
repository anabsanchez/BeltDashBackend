namespace BeltDash.Application.DTOs.Common
{
    /// <summary>
    /// Generic DTO to standardize all API responses.
    /// Provides a consistent structure for success and error in HTTP responses.
    /// </summary>
    /// <typeparam name="T">Type of data to be returned in case of success</typeparam>
    public class ApiResponseDto<T>
    {
        /// <summary>
        /// Indicates whether the operation was completed successfully.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Response data (only valid if Success is true).
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Error message (only valid if Success is false).
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// HTTP status code corresponding to the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Creates a successful response with the provided data.
        /// </summary>
        /// <param name="data">Data to return in the response</param>
        /// <param name="statusCode">HTTP status code (200 by default)</param>
        /// <returns>ApiResponseDto object configured for a successful response</returns>
        public static ApiResponseDto<T> SuccessResponse(T data, int statusCode = 200)
        {
            return new ApiResponseDto<T>
            {
                Success = true,
                Data = data,
                StatusCode = statusCode
            };
        }

        /// <summary>
        /// Creates an error response with the provided message.
        /// </summary>
        /// <param name="errorMessage">Explanatory error message</param>
        /// <param name="statusCode">HTTP status code (400 by default)</param>
        /// <returns>ApiResponseDto object configured for an error response</returns>
        public static ApiResponseDto<T> ErrorResponse(string errorMessage, int statusCode = 400)
        {
            return new ApiResponseDto<T>
            {
                Success = false,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }
}
