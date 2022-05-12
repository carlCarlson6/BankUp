using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BankUp.Backend.Groups.Infrastructure.MongoDb;

public class GroupDocument
{

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = new Guid().ToString();

    public IEnumerable<EventDocument> Events { get; set; } = new List<EventDocument>();
}

public class EventDocument
{
    public string EventType { get; set; }
    public string Payload { get; set; }
}

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
        _ => new Unknown(eventDocument.EventType, eventDocument.Payload)
    })!;
}