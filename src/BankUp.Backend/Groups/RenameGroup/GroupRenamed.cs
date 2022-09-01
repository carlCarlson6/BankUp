using BankUp.Backend.Groups.CreateGroup.Errors;
using BankUp.Backend.Groups.Errors;
using Monads;
using static System.String;

namespace BankUp.Backend.Groups.RenameGroup;

public record GroupRenamed(Guid Id, string Name, DateTime CreatedAt) : IEvent
{
    public static Operation<GroupRenamed> Create(string name) =>
        IsNullOrWhiteSpace(name)
            ? Operation<GroupRenamed>.Ko(new InvalidGroupCreation(new EmptyGroupName()))
            : Operation<GroupRenamed>.Ok(new GroupRenamed(Guid.NewGuid(), name, DateTime.UtcNow));
}