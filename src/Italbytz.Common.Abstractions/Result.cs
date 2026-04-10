namespace Italbytz.Common.Abstractions;

/// <summary>
/// Minimal result wrapper used by several existing ports.
/// </summary>
public sealed class Result<T>
{
    public Result(T value)
    {
        Value = value;
    }

    public T Value { get; }

    public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
    {
        if (mapper == null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        return new Result<TOut>(mapper(Value));
    }
}
