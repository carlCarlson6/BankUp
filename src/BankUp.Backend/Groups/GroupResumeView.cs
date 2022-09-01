using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Groups.RenameGroup;
using BankUp.Backend.Groups.Transactions.AddPayment;

namespace BankUp.Backend.Groups;

public interface IGroupView { }

public record GroupResumeView(
    Guid Id, 
    string Name, 
    IEnumerable<Member> Members,
    IEnumerable<string> CreatedConcepts, 
    uint TotalMoneySpent, 
    DateTime CreatedAt
) : IGroupView
{
    private static readonly GroupResumeView InitialStateView = new(
        new Guid(), string.Empty, new List<Member>(), new List<string>(), 0, new DateTime());
    
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
        PaymentMade payment => view with
        {
            CreatedConcepts = view.CreatedConcepts.Append(payment.Concept),
            TotalMoneySpent = view.TotalMoneySpent + payment.Amount
        },
        _ => view
    };
}
