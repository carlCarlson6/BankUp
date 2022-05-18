using Monads;

namespace BankUp.Backend.Groups.Members;

public class Invitation
{
    public Guid ForGroup { get; }
    public string ForUser { get; }
    public DateTime ValidTill { get; }
    
    public Invitation(Guid forGroup, string forUser, DateTime validTill)
    {
        ForGroup = forGroup;
        ForUser = forUser;
        ValidTill = validTill;
    }
}

public class InvalidInvitation : Error
{ 
    public static Result<T> AsResult<T>() => Result<T>.Ko(new InvalidInvitation());
    
    public InvalidInvitation() : base("Invalid invitation") {}
}