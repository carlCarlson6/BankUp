using Monads;

namespace BankUp.Backend.Groups;

public class Unauthorized : OperationError
{
    public Unauthorized() : base("user is not member of the group") { }
}