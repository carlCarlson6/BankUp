using Monads;

namespace BankUp.Backend.Groups.Errors;

public class EmptyGroupName : OperationError
{
    public EmptyGroupName() : base(nameof(EmptyGroupName)) { }
}