namespace Monads;

public class Result<T>
{
    internal Result() { }

    public static Result<T> Ok(T value) => new OkResult<T>(value);
    public static Result<T> Ko(Error error) => new KoResult<T>(error);
    public static Result<T> Ko(string error) => new KoResult<T>(new Error(error));
}

internal sealed class OkResult<T> : Result<T>
{
    internal readonly T Value;

    internal OkResult(T value) => Value = value;
}

internal sealed class KoResult<T> : Result<T>
{
    internal readonly Error Error;

    internal KoResult(Error error) => Error = error;
}

public class Error : Exception
{
    public Error() { }
    public Error(string message) : base(message) { }
    public Error(string message, Exception exception): base(message, exception) { }

    public override string ToString() => Message;
}

public class NotFoundError : Error 
{
    public NotFoundError(Type type) : base($"element {type.FullName} not found") { }
}