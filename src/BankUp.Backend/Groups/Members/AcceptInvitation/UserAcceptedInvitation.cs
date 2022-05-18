namespace BankUp.Backend.Groups.Members.AcceptInvitation;

public class UserAcceptedInvitation : IEvent
{
    public Guid Id { get; }
    public User User { get; }
    public DateTime CreatedAt { get; }
}