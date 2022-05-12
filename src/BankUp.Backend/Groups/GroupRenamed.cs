using Monads;
using Newtonsoft.Json;
using static System.String;

namespace BankUp.Backend.Groups;

public class GroupRenamed : IEvent
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }
    
    [JsonConstructor]
    public GroupRenamed(Guid id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }
    
    public static Result<GroupRenamed> Create(string name) =>
        IsNullOrWhiteSpace(name)
            ? Result<GroupRenamed>.Ko(new InvalidGroupCreation())
            : Result<GroupRenamed>.Ok(new GroupRenamed(Guid.NewGuid(), name, DateTime.UtcNow));
}