using Domain.Agreggates;
using Domain.Common;
using Domain.IRepository;
using Infrastructure.Dapper;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    #region Properties

    private readonly UserContext _context;
    public IUnitOfWork UnitOfWork { get => _context; }
    #endregion Properties

    #region Constructors

    public UserRepository(UserContext context, IDatabaseConnectionFactory database)
    {
        if (database is null)
            throw new ArgumentNullException(nameof(database));

        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    #endregion Constructors

    public User Create(User user)
    {
        return _context.Users.Add(user).Entity;
    }

    public async Task<bool> Delete(User user)
    {
        _context.Users.Remove(user);

        return await _context.SaveEntitiesAsync();
    }

    public async Task<bool> Update(User user)
    {
        _context.Users.Update(user);

        return await _context.SaveEntitiesAsync();
    }

    public User GetUser(int userId)
    {
        return _context.Users.SingleOrDefault(x => x.UserId == userId);
    }
}