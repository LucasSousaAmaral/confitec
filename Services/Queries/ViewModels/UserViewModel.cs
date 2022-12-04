using Domain.Common;

namespace Services.Queries.ViewModels;

public record UserDto
{
    #region Properties

    public int UserId { get; init; }
    public string UserName { get; init; }
    public string SurName { get; init; }
    public string Email { get; init; }
    public DateTime BirthDate { get; init; }
    public Scholarity Scholarity { get; init; }
    public string ScholarityName { get; init; }

    #endregion Properties

    #region Constructors

    public UserDto(int userId, string userName, string surName, string email, DateTime birthDate, Scholarity scholarity, string scholarityName)
    {
        UserId = userId;
        UserName = userName;
        SurName = surName;
        Email = email;
        BirthDate = birthDate;
        Scholarity = scholarity;
        ScholarityName = scholarityName;
    }

    #endregion Constructors
}