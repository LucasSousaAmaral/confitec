using Domain.Agreggates;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.EntityFramework;

public class UserContext : DbContext, IUnitOfWork
{
    #region Properties
    private readonly IMediator _mediator;
    private IDbContextTransaction? _currentTransaction;
    public const string DEFAULT_SCHEMA = "dbo";

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=confitec;persist security info=True;Integrated Security=true;Trusted_Connection=True; Encrypt=False;");
        }
    }
    #endregion

    #region Constructors
    public UserContext(DbContextOptions<UserContext> options)
        : base(options) { }
    public UserContext(DbContextOptions<UserContext> options,
        IMediator mediator, IHttpContextAccessor httpContext) : base(options)
        => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    #endregion

    #region Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;
        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return _currentTransaction;
    }
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        ValidateTransaction(transaction);

        try
        {
            await SaveChangesAsync();
            transaction?.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync(transaction);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
    public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
    {
        try
        {
            await _currentTransaction?.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    private void ValidateTransaction(IDbContextTransaction? transaction)
    {
        if (transaction is null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction?.TransactionId} is not current transaction.");
    }
    #endregion Methods
}