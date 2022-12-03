namespace Domain.Common;
public interface IAggregateRoot 
{
}
public interface IRepository<T> where T : IAggregateRoot
{
    #region Properties

    IUnitOfWork UnitOfWork { get; }

    #endregion
}