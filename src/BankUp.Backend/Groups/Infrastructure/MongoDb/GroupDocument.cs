using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
    public string EventType { get; set; } // TODO - move as new class EventDocumentMetadata
    public string Payload { get; set; }
}