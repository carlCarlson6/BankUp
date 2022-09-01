using Monads;

namespace BankUp.Backend.Groups.CreateGroup.Errors;

public class NoInitialOwner : OperationError
{
    public NoInitialOwner() : base(nameof(NoInitialOwner)) { }
}