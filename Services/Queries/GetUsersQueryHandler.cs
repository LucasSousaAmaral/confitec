using Dapper;
using Infrastructure.Dapper;
using MediatR;
using Services.Queries.ViewModels;
using System.Data;

namespace Services.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    #region Properties

    private readonly IDatabaseConnectionFactory _database;

    #endregion Properties

    #region Constructors

    public GetUsersQueryHandler(IDatabaseConnectionFactory database)
    {
        if (database is null)
            throw new ArgumentNullException(nameof(database));

        _database = database;
    }

    #endregion Constructors

    #region Methods

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT us.[UserId]
                               ,us.[UserName]
                               ,us.[SurName]
                               ,us.[Email]
                               ,us.[BirthDate]
                               ,us.[Scholarity]
	                           ,sc.[ScholarityName] as ScholarityName
                        FROM [Users] as us
                        INNER JOIN Scholarity as sc on us.Scholarity = sc.ScholarityId";

        var parameters = new DynamicParameters();

        using var connection = await _database.CreateConnectionAsync();

        IEnumerable<UserDto> users = await connection.QueryAsync<UserDto>(sql: sql, param: parameters, commandType: CommandType.Text);

        return users;
    }

    #endregion Methods
}