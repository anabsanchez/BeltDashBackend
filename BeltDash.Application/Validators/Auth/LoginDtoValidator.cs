using BeltDash.Application.DTOs.Auth;
using FluentValidation;

namespace BeltDash.Application.Validators.Auth
{
    /// <summary>
    /// Validator for the login DTO.
    /// Applies validation rules to authentication data.
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
