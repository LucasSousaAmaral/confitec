using FluentValidation;
using Services.Commands;

namespace Services.Validations.UserValidations;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(p => p.UserId).NotNull().NotEmpty();
    }
}
