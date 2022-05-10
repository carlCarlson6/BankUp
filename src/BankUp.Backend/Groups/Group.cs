using System.Text.Json.Serialization;

namespace BankUp.Backend.Groups;

public class Group
{
    public Guid Id { get; }
    public IEnumerable<IEvent> Events { get; }

    [JsonConstructor] 
    public Group(Guid id, IEnumerable<IEvent> events) => (Id, Events) = (id, events);
    public Group(GroupCreated groupCreated) => (Id, Events) = (groupCreated.GroupId, new List<IEvent>{ groupCreated });
}

public class GroupModel
{
    public string Id { get; set; }
    public IEnumerable<IEvent> Events { get; set; }

    public static GroupModel FromDomain(Group group) => new()
    {
        Id = group.Id.ToString(),
        Events = group.Events
    };
}