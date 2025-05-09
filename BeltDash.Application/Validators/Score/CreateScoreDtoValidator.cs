using BeltDash.Application.DTOs.Score;
using FluentValidation;

namespace BeltDash.Application.Validators.Score
{
    public class CreateScoreDtoValidator : AbstractValidator<CreateScoreDto>
    {
        public CreateScoreDtoValidator()
        {
            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Points cannot be negative.");
        }
    }
}
