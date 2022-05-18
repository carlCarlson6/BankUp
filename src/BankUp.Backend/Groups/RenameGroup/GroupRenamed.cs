using BankUp.Backend.Groups.CreateGroup;
using Monads;
using Newtonsoft.Json;

namespace BankUp.Backend.Groups.RenameGroup;

public class GroupRenamed : IEvent
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }

    [JsonConstructor]
    public GroupRenamed(Guid id, string name, DateTime createdAt) => (Id, Name, CreatedAt) = (id, name, createdAt);
    
    public static Result<GroupRenamed> Create(string name) =>
        String.IsNullOrWhiteSpace(name)
            ? Result<GroupRenamed>.Ko(new InvalidGroupCreation()) // TODO - create proper error
            : Result<GroupRenamed>.Ok(new GroupRenamed(Guid.NewGuid(), name, DateTime.UtcNow));
}