namespace BankUp.Backend.Groups;

public record Member(Guid Id, string Name)
{
    public static Member NewGroupMember(string name) => new Member(Guid.NewGuid(), name);
}