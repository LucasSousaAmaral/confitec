using Domain.Agreggates;
using Domain.Common;
using Domain.Exceptions;
using Domain.IRepository;
using MediatR;
using Services.Queries.ViewModels;
using Services.Validations.UserValidations;
using System.Net;

namespace Services.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    #region Properties

    private readonly IUserRepository _userRepository;

    #endregion Properties

    #region Constructors

    public CreateUserCommandHandler(IUserRepository userRepository) : base()
    {
        _userRepository = userRepository;
    }

    #endregion Constructors


    #region Methods

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            throw new UsersValidationException(result.Errors, typeof(UpdateUserCommand).Name, HttpStatusCode.BadRequest);
        }

        var user = new User(request.UserName, request.SurName, request.Email, request.BirthDate, request.Scholarity);

        _userRepository.Create(user);
        await _userRepository.UnitOfWork.SaveEntitiesAsync();

        var userDto = new UserDto(user.UserId, user.UserName, user.SurName, user.Email, user.BirthDate, user.Scholarity, Enum.GetName(typeof(Scholarity), user.Scholarity));

        return userDto;
    }

    #endregion Methods
}
