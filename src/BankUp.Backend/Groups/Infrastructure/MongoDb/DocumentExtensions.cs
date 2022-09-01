using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Groups.RenameGroup;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BankUp.Backend.Groups.Infrastructure.MongoDb;

public static class DocumentExtensions
{
    public static GroupDocument ToDocument(this Group group) => new(){ Id = group.Id.ToString(), Events = group.Events.ToDocuments()};
    public static Group ToDomain(this GroupDocument document) => new(
        Guid.Parse(document.Id), 
        document.Events.Select(eventDocument => eventDocument.ToDomain()).ToList());

    private static EventDocument ToDocument(this IEvent @event) => new()
    {
        EventType = @event.GetType().Name,
        Payload = JsonConvert.SerializeObject(@event)
    };

    private static IEnumerable<EventDocument> ToDocuments(this IEnumerable<IEvent> events) => 
        events.Select(@event => @event.ToDocument());

    private static IEvent ToDomain(this EventDocument eventDocument) => (eventDocument.EventType switch
    {
        nameof(GroupCreated) => JsonSerializer.Deserialize<GroupCreated>(eventDocument.Payload),
        nameof(GroupRenamed) => JsonSerializer.Deserialize<GroupRenamed>(eventDocument.Payload),
        // TODO - add rest of events
        _ => new UnknownEvent(eventDocument.EventType, eventDocument.Payload)
    })!;
}