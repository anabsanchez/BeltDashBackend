using BeltDash.Application.DTOs.Score;
using FluentValidation;

namespace BeltDash.Application.Validators.Score
{
    /// <summary>
    /// Validator for the score creation DTO.
    /// Ensures that score data is valid before being processed.
    /// </summary>
    public class CreateScoreDtoValidator : AbstractValidator<CreateScoreDto>
    {
        public CreateScoreDtoValidator()
        {
            // Points validation
            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Points cannot be negative.");
        }
    }
}
