namespace Monads;

public static class GenericExtensions
{
    public static TOut Map<TIn, TOut>(this TIn input, Func<TIn, TOut> map) => map(input);
}