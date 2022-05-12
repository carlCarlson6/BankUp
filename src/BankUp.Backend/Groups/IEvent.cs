using System.Text.Json.Serialization;

namespace BankUp.Backend.Groups;

public interface IEvent { }

public class Unknown : IEvent
{
    public string EventType { get; }
    public string Payload { get;  }

    [JsonConstructor]
    public Unknown(string eventType, string payload)
    {
        EventType = eventType;
        Payload = payload;
    }
}

public interface ITransaction : IEvent { }

public static class EventExtensions
{
    public static IEnumerable<Guid> GetMembers(this IEnumerable<IEvent> events) => 
        events.Aggregate(new List<Guid>(), GetMembers);

    private static List<Guid> GetMembers(List<Guid> members, IEvent @event) => @event switch
    {
        GroupCreated groupCreated => members.Concat(groupCreated.Members).ToList(),
        _ => members
    };
}