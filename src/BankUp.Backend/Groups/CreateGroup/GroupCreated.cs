using System.Text.Json.Serialization;
using Monads;
using static System.String;

namespace BankUp.Backend.Groups.CreateGroup;

public class GroupCreated : IEvent
{
    public Guid Id { get; }
    public Guid GroupId { get; }
    public string Name { get; }
    public IEnumerable<User> Members { get; }
    public DateTime CreatedAt { get; }

    [JsonConstructor]
    public GroupCreated(Guid id, Guid groupId, string name, IEnumerable<User> members, DateTime createdAt) =>
        (Id, GroupId, Name, Members, CreatedAt) = (id, groupId, name, members, createdAt);

    public static Result<GroupCreated> Create(string name, List<User> members) =>
        IsNullOrWhiteSpace(name) || !members.Any()
            ? Result<GroupCreated>.Ko(new InvalidGroupCreation())
            : Result<GroupCreated>.Ok(new GroupCreated(Guid.NewGuid(), Guid.NewGuid(), name, members, DateTime.UtcNow));
}

public class InvalidGroupCreation : Error
{
    public InvalidGroupCreation() : base(nameof(InvalidGroupCreation)) { }
}