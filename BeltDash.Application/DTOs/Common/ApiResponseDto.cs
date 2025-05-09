namespace BeltDash.Application.DTOs.Common
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }

        public T? Data { get; set; }

        public string? ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public static ApiResponseDto<T> SuccessResponse(T data, int statusCode = 200)
        {
            return new ApiResponseDto<T>
            {
                Success = true,
                Data = data,
                StatusCode = statusCode
            };
        }

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
