using BankUp.Backend.Groups.CreateGroup.Errors;
using BankUp.Backend.Groups.Errors;
using Monads;
using static System.String;

namespace BankUp.Backend.Groups.CreateGroup;

public record GroupCreated(
    Guid Id, 
    Guid GroupId, 
    string Name, 
    IEnumerable<Member> Members, 
    IEnumerable<User> Owners, 
    DateTime CreatedAt) : IEvent
{
    public static Operation<GroupCreated> Create(string name, List<string> members, List<User> owners)
    {
        var errors = ValidateInput(name, members, owners);
        return errors.Any() 
            ? Operation<GroupCreated>.Ko(new InvalidGroupCreation(errors)) 
            : Operation<GroupCreated>.Ok(new GroupCreated(
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                name, 
                members.Select(Member.NewGroupMember), owners,
                DateTime.UtcNow));
    }

    private static List<OperationError> ValidateInput(string name, List<string> members, List<User> owners)
    {
        var errors = new List<OperationError>();
        if (IsNullOrWhiteSpace(name))
            errors.Add(new EmptyGroupName());
        
        if (!owners.Any())
            errors.Add(new NoInitialOwner());
        
        return errors;
    }
}