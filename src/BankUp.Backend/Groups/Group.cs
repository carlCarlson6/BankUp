using BankUp.Backend.Groups.CreateGroup;
using Monads;

namespace BankUp.Backend.Groups;

public record Group
{
    public Guid Id { get; }
    public List<IEvent> Events { get; private init; }
    
    public Group(Guid id, List<IEvent> events) => (Id, Events) = (id, events);

    public Group(List<IEvent> events) => (Id, Events) = (
        ((GroupCreated)events.FirstOrDefault(@event => @event is GroupCreated)!).GroupId, events);
    
    public Group(GroupCreated groupCreated) => (Id, Events) = (groupCreated.GroupId, new List<IEvent>{ groupCreated });

    public Group Add(IEvent @event) => this with { Events = Events.Append(@event).ToList() };

    public Result<Group> IsUserAMember(User user) => Events.GetMembers().Contains(user)
        ? Result<Group>.Ok(this)
        : Result<Group>.Ko(new Unauthorized());
}