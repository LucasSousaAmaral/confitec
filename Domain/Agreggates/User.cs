using Domain.Common;

namespace Domain.Agreggates;

public class User : IAggregateRoot
{
    #region Properties

    public int UserId { get; private set; }
    public string UserName { get; private set; }
    public string SurName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public virtual Scholarity Scholarity { get; private set; }

    #endregion Properties

    #region Constructors
    protected User()
    {
    }

    public User(string userName, string surName, string email, DateTime birthDate, Scholarity scholarity) : this()
    {
        UserName = userName;
        SurName = surName;
        Email = email;
        BirthDate = birthDate;
        Scholarity = scholarity;
    }

    public User UpdateUser(string userName, string surName, string email, DateTime birthDate, Scholarity scholarity)
    {
        UserName = userName;
        SurName = surName;
        Email = email;
        BirthDate = birthDate;
        Scholarity = scholarity;

        return this;
    }

    #endregion Constructors
}