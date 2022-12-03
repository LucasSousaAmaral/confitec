using Domain.Agreggates;
using Domain.IRepository;
using MediatR;
using Services.Queries.ViewModels;
using Services.Validations.UserValidations;

namespace Services.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    #region Properties

    private readonly IUserRepository _userRepository;

    #endregion Properties

    #region Constructors

    public DeleteUserCommandHandler(IUserRepository userRepository) : base()
    {
        _userRepository = userRepository;
    }

    #endregion Constructors


    #region Methods

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
        var result = validator.Validate(request);

        if (!result.IsValid) 
        {
            throw new FluentValidation.ValidationException(result.Errors);
        }

        var user = _userRepository.GetUser(request.UserId);

        await _userRepository.Delete(user);

        return await _userRepository.UnitOfWork.SaveEntitiesAsync(); ;
    }

    #endregion Methods
}
