namespace Italbytz.Common.Abstractions;

/// <summary>
/// A data sink providing create, update, and delete operations.
/// </summary>
public interface IDataSink<TId, TEntity>
{
    /// <summary>
    /// Creates an entity.
    /// </summary>
    Task<TId?> Create(TEntity entity);

    /// <summary>
    /// Updates an entity.
    /// </summary>
    Task<bool> Update(TId id, TEntity entity);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    Task<bool> Delete(TId id);
}
