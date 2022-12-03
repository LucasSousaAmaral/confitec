using Domain.Agreggates;
using Domain.Common;

namespace Domain.IRepository;

public interface IUserRepository : IRepository<User>
{
    #region Methods
    User Create(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(User user);
    User GetUser(int userId);

    #endregion Methods
}