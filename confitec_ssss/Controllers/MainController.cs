using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Commands;
using Services.Helpers;
using Services.Queries;
using Services.Queries.ViewModels;

namespace confitec_ssss.Controllers;

[Route(template: ApiActionsV1.Main)]
[ApiController]
public class MainController : ControllerBase
{
    #region Properties
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public MainController(IMediator mediator)
        => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    #endregion

    #region Methods
    [HttpGet(template: ApiActionsV1.GetUsers, Name = nameof(ApiActionsV1.GetUsers))]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] GetUsersQuery getUsersQuery)
        => Ok(await _mediator.Send(request: getUsersQuery));

    [HttpPost(ApiActionsV1.CreateUser, Name = nameof(ApiActionsV1.CreateUser))]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand createUserCommand)
        => CreatedAtAction(nameof(CreateUser), await _mediator.Send(createUserCommand));

    [HttpPut(ApiActionsV1.UpdateUser, Name = nameof(ApiActionsV1.UpdateUser))]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        => Ok(await _mediator.Send(updateUserCommand));
    [HttpDelete(ApiActionsV1.DeleteUser, Name = nameof(ApiActionsV1.DeleteUser))]
    public async Task<ActionResult<bool>> UpdateUser([FromQuery] DeleteUserCommand deleteUserCommand)
        => Ok(await _mediator.Send(deleteUserCommand));

    #endregion
}
