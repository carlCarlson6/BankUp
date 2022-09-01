using Monads;

namespace BankUp.Backend.Groups;

public interface IUseCase<in TInput, TOutput>
{
    public Task<Operation<TOutput>> Execute(TInput input);
}