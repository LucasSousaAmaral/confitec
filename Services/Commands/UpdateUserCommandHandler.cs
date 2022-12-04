using Domain.Common;
using Domain.IRepository;
using MediatR;
using Services.Queries.ViewModels;
using Services.Validations.UserValidations;

namespace Services.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    #region Properties

    private readonly IUserRepository _userRepository;

    #endregion Properties

    #region Constructors

    public UpdateUserCommandHandler(IUserRepository userRepository) : base()
    {
        _userRepository = userRepository;
    }

    #endregion Constructors


    #region Methods

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        UpdateUserCommandValidator validator = new UpdateUserCommandValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            throw new FluentValidation.ValidationException(result.Errors);
        }

        var user = _userRepository.GetUser(request.UserId);

        user.UpdateUser(request.UserName, request.SurName, request.Email, request.BirthDate, request.Scholarity);

        await _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync();

        var userDto = new UserDto(user.UserId, user.UserName, user.SurName, user.Email, user.BirthDate, user.Scholarity, Enum.GetName(typeof(Scholarity), user.Scholarity));

        return userDto;
    }

    #endregion Methods
}
