using System.Text.Json.Serialization;
using BankUp.Backend.Groups.CreateGroup;

namespace BankUp.Backend.Groups;

public interface IEvent { }

public class UnknownEvent : IEvent
{
    public string EventType { get; }
    public string Payload { get;  }

    [JsonConstructor]
    public UnknownEvent(string eventType, string payload)
    {
        EventType = eventType;
        Payload = payload;
    }
}

public static class EventExtensions
{
    public static IEnumerable<User> GetMembers(this IEnumerable<IEvent> events) => 
        events.Aggregate(new List<User>(), GetMembers);

    private static List<User> GetMembers(List<User> members, IEvent @event) => @event switch
    {
        GroupCreated groupCreated => members.Concat(groupCreated.Members).ToList(),
        _ => members
    };
}