namespace Italbytz.Common.Abstractions;

/// <summary>
/// Synchronous service contract with input and output.
/// </summary>
public interface IService<in TInDto, out TOutDto>
{
    TOutDto Execute(TInDto inDto);
}

/// <summary>
/// Synchronous service contract without input.
/// </summary>
public interface IService<out TOutDto>
{
    TOutDto Execute();
}

/// <summary>
/// Asynchronous service contract with input and output.
/// </summary>
public interface IAsyncService<in TInDto, TOutDto>
{
    Task<TOutDto> Execute(TInDto inDto);
}

/// <summary>
/// Asynchronous service contract without input.
/// </summary>
public interface IAsyncService<TOutDto>
{
    Task<TOutDto> Execute();
}
