using FluentValidation;
using Services.Commands;

namespace Services.Validations.UserValidations;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.Email).NotNull().EmailAddress();
        RuleFor(p => p.BirthDate).NotNull().LessThan(DateTime.Today);
        RuleFor(p => p.Scholarity).NotNull().IsInEnum();
    }
}
