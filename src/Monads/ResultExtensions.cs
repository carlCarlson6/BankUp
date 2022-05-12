namespace Monads;

public static class ResultExtensions
{
    public static Result<TOut> Map<T, TOut>(this Result<T> result, Func<T, TOut> mapper) => result switch
    {
        KoResult<T> koResult => new KoResult<TOut>(koResult.Error),
        OkResult<T> okResult => Result<TOut>.Ok(mapper(okResult.Value)),
        _ => throw new ArgumentOutOfRangeException(nameof(result))
    };

    public static Task<Result<TOut>> Map<T, TOut>(this Result<T> result, Func<T, Task<TOut>> mapper) => result switch
    {
        KoResult<T> koResult => Task.FromResult(Result<TOut>.Ko(koResult.Error)),
        OkResult<T> okResult => ExecuteAsyncMap(okResult, mapper),
        _ => throw new ArgumentOutOfRangeException(nameof(result))
    };
    
    private static async Task<Result<TOut>> ExecuteAsyncMap<T, TOut>(OkResult<T> okResult, Func<T, Task<TOut>> mapper) => Result<TOut>.Ok(await mapper(okResult.Value));

    public static TOut Map<T, TOut>(this Result<T> result, Func<T, TOut> onOkResult, Func<Error, TOut> onKoResult) => result switch
    {
        KoResult<T> koResult => onKoResult(koResult.Error),
        OkResult<T> okResult => onOkResult(okResult.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(result))
    };

    public static Result<T> UnWrap<T>(this Result<Result<T>> resultResult) => resultResult switch
    {
        KoResult<Result<T>> koResult => new KoResult<T>(koResult.Error),
        OkResult<Result<T>> okResult => okResult.Value,
        _ => throw new ArgumentOutOfRangeException(nameof(resultResult))
    };
}