using MediatR;

namespace Services.Commands;

public record DeleteUserCommand(int UserId) : IRequest<bool>;