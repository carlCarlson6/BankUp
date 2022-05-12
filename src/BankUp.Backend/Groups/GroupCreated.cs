using System.Text.Json.Serialization;
using Monads;
using static System.String;

namespace BankUp.Backend.Groups;

public class GroupCreated : IEvent
{
    public Guid Id { get; }
    public Guid GroupId { get; }
    public string Name { get; }
    public IEnumerable<Guid> Members { get; }
    public DateTime CreatedAt { get; }
 
    [JsonConstructor]
    public GroupCreated(Guid id, Guid groupId, string name, IEnumerable<Guid> members, DateTime createdAt)
    {
        Id = id;
        GroupId = groupId;
        Name = name;
        Members = members;
        CreatedAt = createdAt;
    }

    public static Result<GroupCreated> Create(string name, List<Guid> members) =>
        IsNullOrWhiteSpace(name) || !members.Any()
            ? Result<GroupCreated>.Ko(new InvalidGroupCreation())
            : Result<GroupCreated>.Ok(new GroupCreated(Guid.NewGuid(), Guid.NewGuid(), name, members, DateTime.UtcNow));
}

public class InvalidGroupCreation : Error
{
    public InvalidGroupCreation() : base(nameof(InvalidGroupCreation)) { }
}

public class GroupCreatedModel : IEvent
{
    public string GroupId { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Members { get; set; }
}