namespace Monads;

public static class OperationExtensions
{
    public static Operation<TOut> Map<T, TOut>(this Operation<T> operation, Func<T, TOut> mapper) => operation switch
    {
        KoResult<T> koResult => new KoResult<TOut>(koResult.OperationError),
        OkResult<T> okResult => Operation<TOut>.Ok(mapper(okResult.Value)),
        _ => throw new ArgumentOutOfRangeException(nameof(operation))
    };

    public static Task<Operation<TOut>> Map<T, TOut>(this Operation<T> operation, Func<T, Task<TOut>> mapper) => operation switch
    {
        KoResult<T> koResult => Task.FromResult(Operation<TOut>.Ko(koResult.OperationError)),
        OkResult<T> okResult => ExecuteAsyncMap(okResult, mapper),
        _ => throw new ArgumentOutOfRangeException(nameof(operation))
    };
    
    private static async Task<Operation<TOut>> ExecuteAsyncMap<T, TOut>(OkResult<T> okResult, Func<T, Task<TOut>> mapper) => Operation<TOut>.Ok(await mapper(okResult.Value));

    public static TOut Map<T, TOut>(this Operation<T> operation, Func<T, TOut> onOkResult, Func<OperationError, TOut> onKoResult) => operation switch
    {
        KoResult<T> koResult => onKoResult(koResult.OperationError),
        OkResult<T> okResult => onOkResult(okResult.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(operation))
    };

    public static Operation<T> UnWrap<T>(this Operation<Operation<T>> operationOperation) => operationOperation switch
    {
        KoResult<Operation<T>> koResult => new KoResult<T>(koResult.OperationError),
        OkResult<Operation<T>> okResult => okResult.Value,
        _ => throw new ArgumentOutOfRangeException(nameof(operationOperation))
    };
}