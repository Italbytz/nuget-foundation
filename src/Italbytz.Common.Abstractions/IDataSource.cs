namespace Italbytz.Common.Abstractions;

/// <summary>
/// A data source providing read access to entities.
/// </summary>
public interface IDataSource<TId, TEntity>
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    Task<TEntity?> Retrieve(TId id);

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    Task<List<TEntity>?> RetrieveAll();
}
