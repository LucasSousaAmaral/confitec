using Domain.Common;
using MediatR;
using Services.Queries.ViewModels;
using System.Runtime.Serialization;

namespace Services.Commands;

[DataContract]
public record CreateUserCommand : IRequest<UserDto>
{
    [DataMember]
    public string UserName { get; init; }
    [DataMember]
    public string SurName { get; init; }
    [DataMember]
    public string Email { get; init; }
    [DataMember]
    public DateTime BirthDate { get; init; }
    [DataMember]
    public Scholarity Scholarity { get; init; }

}