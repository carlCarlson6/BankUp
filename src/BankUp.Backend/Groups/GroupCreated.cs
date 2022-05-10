using Monads;
using static System.String;

namespace BankUp.Backend.Groups;

public class GroupCreated : IEvent
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> Members { get; set; }
 
    private GroupCreated(Guid groupId, string name, IEnumerable<Guid> members)
    {
        GroupId = groupId;
        Name = name;
        Members = members;
    }

    public static Result<GroupCreated> Create(string name, List<Guid> members) =>
        IsNullOrWhiteSpace(name) || !members.Any()
            ? Result<GroupCreated>.Ko(new InvalidGroupCreation())
            : Result<GroupCreated>.Ok(new GroupCreated(Guid.NewGuid(), name, members));
}

public class InvalidGroupCreation : Error
{
    public InvalidGroupCreation() : base(nameof(InvalidGroupCreation)) { }
}