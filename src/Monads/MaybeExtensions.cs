namespace Monads;

public static class MaybeExtensions
{
    public static Maybe<TOut> Map<T, TOut>(this Maybe<T> maybe, Func<T, TOut> mapper) => maybe switch
    {
        None<T> => new None<TOut>(),
        Some<T> some => Maybe<TOut>.Some(mapper(some.Value)),
        _ => throw new ArgumentOutOfRangeException(nameof(maybe))
    };
    
    public static TOut Map<T, TOut>(this Maybe<T> maybe, Func<T, TOut> onSome, Func<TOut> onNone) => maybe switch
    {
        None<T> => onNone(),
        Some<T> some => onSome(some.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(maybe))
    };

    public static Maybe<T> UnWrap<T>(this Maybe<Maybe<T>> maybeMaybe) => maybeMaybe switch
    {
        None<Maybe<T>> => new Maybe<T>(),
        Some<Maybe<T>> some => some.Value,
        _ => throw new ArgumentOutOfRangeException(nameof(maybeMaybe))
    };

    public static Result<T> ToResult<T>(this Maybe<T> maybe) => maybe switch
    {
        None<T> _ => Result<T>.Ko(new NotFoundError(typeof(T))),
        Some<T> some => Result<T>.Ok(some.Value),
        _ => throw new ArgumentOutOfRangeException(nameof(maybe))
    };
    
    public static Maybe<T> Find<T>(this IEnumerable<T> sourceList, Func<T, bool> predicate)
    {
        var elementFound = sourceList.FirstOrDefault(predicate);
        return elementFound is null ? Maybe<T>.None() : Maybe<T>.Some(elementFound);
    }
}