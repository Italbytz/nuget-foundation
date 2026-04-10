namespace Italbytz.Common.Abstractions;

/// <summary>
/// Repository contract with create, read, update, and delete operations.
/// </summary>
public interface ICrudRepository<TId, TEntity> :
    IRepository<TId, TEntity>,
    IDataSource<TId, TEntity>,
    IDataSink<TId, TEntity>
{
}
