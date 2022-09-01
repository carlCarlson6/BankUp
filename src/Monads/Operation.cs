namespace Monads;

public class Operation<T>
{
    internal Operation() { }

    public static Operation<T> Ok(T value) => new OkResult<T>(value);
    public static Operation<T> Ko(OperationError operationError) => new KoResult<T>(operationError);
    public static Operation<T> Ko(string error) => new KoResult<T>(new OperationError(error));
}

internal sealed class OkResult<T> : Operation<T>
{
    internal readonly T Value;

    internal OkResult(T value) => Value = value;
}

internal sealed class KoResult<T> : Operation<T>
{
    internal readonly OperationError OperationError;

    internal KoResult(OperationError operationError) => OperationError = operationError;
}

public class OperationError : Exception
{
    public OperationError() { }
    public OperationError(string message) : base(message) { }
    public OperationError(string message, Exception exception): base(message, exception) { }

    public override string ToString() => Message;
}

public class NotFoundOperationError : OperationError 
{
    public NotFoundOperationError(Type type) : base($"element {type.FullName} not found") { }
}