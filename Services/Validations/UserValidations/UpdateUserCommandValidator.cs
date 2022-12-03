using FluentValidation;
using Services.Commands;

namespace Services.Validations.UserValidations;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.Email).NotNull().EmailAddress();
        RuleFor(p => p.BirthDate).NotNull().LessThan(DateTime.Today);
        RuleFor(p => p.Scholarity).NotNull().IsInEnum();
    }
}
