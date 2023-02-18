using FluentValidation;
using ResumeFormatter.Domain.Entities;

namespace ResumeFormatter.Service.Validators
{
    public class KeywordsValidator : AbstractValidator<Keyword>
    {
        public KeywordsValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Please enter the UserId.")
                .NotNull().WithMessage("Please enter the UserId.");

            RuleFor(c => c.Word)
                .NotEmpty().WithMessage("Please enter the Keywords.")
                .NotNull().WithMessage("Please enter the Keywords.");
        }
    }
}
