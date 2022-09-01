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
    public static IEnumerable<User> GetOwners(this IEnumerable<IEvent> events) => 
        events.Aggregate(new List<User>(), GetOwners);

    private static List<User> GetOwners(List<User> owners, IEvent @event) => @event switch
    {
        GroupCreated groupCreated => owners.Concat(groupCreated.Owners).ToList(),
        _ => owners
    };
}