using Engenharia.Domain.Entities.Identity;
using FluentValidation;
using System;

namespace Engenharia.Service.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(r => r)
               .NotNull()
               .OnAnyFailure(x =>
               {
                   throw new ArgumentNullException("Can't found the object.");
               });

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("É necessário informar o nome.")
                .NotNull().WithMessage("É necessário informar o nome.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("É necessário informar a descrição.")
                .NotNull().WithMessage("É necessário informar a descrição.");

        }
    }
}
