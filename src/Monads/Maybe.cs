namespace Monads;

public class Maybe<T>
{
    internal Maybe() { }

    private static Maybe<T> Create(T? value) => value is null ? new None<T>() : new Some<T>(value);

    public static Maybe<T> Some(T value) => Create(value);
    public static Maybe<T> None() => new None<T>();
}

internal sealed class Some<T> : Maybe<T>
{
    internal readonly T Value;

    internal Some(T value) => Value = value;
}

internal sealed class None<T> : Maybe<T> { }