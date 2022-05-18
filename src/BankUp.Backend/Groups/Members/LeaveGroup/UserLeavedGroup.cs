using System.Text.Json.Serialization;

namespace BankUp.Backend.Groups.Members.LeaveGroup;

public class UserLeavedGroup : IEvent
{
    public Guid Id { get; }
    public User Member { get; }
    public DateTime CreatedAt { get; }

    [JsonConstructor]
    public UserLeavedGroup(Guid id, User member, DateTime createdAt) =>
        (Id, Member, CreatedAt) = (id, member, createdAt);

    public static UserLeavedGroup Create(User member) => new(Guid.NewGuid(), member, DateTime.UtcNow);
}