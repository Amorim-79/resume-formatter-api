using FluentValidation;
using ResumeFormatter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeFormatter.Service.Validators
{
    public class KeywordsValidator : AbstractValidator<Keywords>
    {
        public KeywordsValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("Please enter the UserId.")
                .NotNull().WithMessage("Please enter the UserId.");

            RuleFor(c => c.Keyword)
                .NotEmpty().WithMessage("Please enter the Keywords.")
                .NotNull().WithMessage("Please enter the Keywords.");
        }
    }
}
