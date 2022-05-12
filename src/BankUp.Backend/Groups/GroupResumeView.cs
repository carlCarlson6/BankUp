namespace BankUp.Backend.Groups;

public interface IGroupView { }

public record GroupResumeView(Guid Id, string Name, IEnumerable<Guid> Members, DateTime CreatedAt) : IGroupView
{
    private static readonly GroupResumeView InitialStateView = new(new Guid(), string.Empty, new List<Guid>(), new DateTime());
    
    public static GroupResumeView BuildView(IEnumerable<IEvent> events) => events.Aggregate(InitialStateView, UpdateView);
    
    public static GroupResumeView UpdateView(GroupResumeView view, IEvent @event) => @event switch
    {
        GroupCreated created => view with
        {
            Id = created.GroupId, 
            Name = created.Name,
            Members = created.Members,
            CreatedAt = created.CreatedAt
        },
        GroupRenamed renamed => view with { Name = renamed.Name },
        _ => view
    };
}
