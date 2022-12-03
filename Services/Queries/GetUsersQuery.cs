using MediatR;
using Services.Queries.ViewModels;

namespace Services.Queries;

public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;