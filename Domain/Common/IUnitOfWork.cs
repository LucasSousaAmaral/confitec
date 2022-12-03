using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Common;

public interface IUnitOfWork : IDisposable
{
        #region Methods
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollbackTransactionAsync(IDbContextTransaction? transaction);
        #endregion
}