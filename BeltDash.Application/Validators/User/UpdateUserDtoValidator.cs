using BeltDash.Application.DTOs.User;
using FluentValidation;

namespace BeltDash.Application.Validators.User
{
    /// <summary>
    /// Validator for the user update DTO.
    /// Applies validation rules for the data that can be modified in an existing user.
    /// </summary>
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            // Username validation
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(25).WithMessage("Username must be less than 25 characters.");

            // Email validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
        }
    }
}
