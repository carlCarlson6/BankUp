using BankUp.Backend.Groups.CreateGroup;
using BankUp.Backend.Groups.Members.AcceptInvitation;
using BankUp.Backend.Groups.Members.LeaveGroup;
using BankUp.Backend.Groups.RenameGroup;
using BankUp.Backend.Groups.Transactions.AddPayment;
using Monads;

namespace BankUp.Backend.Groups;

public interface IGroupView { }

public record GroupResumeView(
    Guid Id, 
    string Name, 
    IEnumerable<User> Members, 
    IEnumerable<string> CreatedConcepts, 
    uint TotalMoneySpent, 
    DateTime CreatedAt
) : IGroupView
{
    private static readonly GroupResumeView InitialStateView = new(
        new Guid(), string.Empty, new List<User>(), new List<string>(), 0, new DateTime());
    
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
        UserAcceptedInvitation acceptedInvitation => view with { Members = view.Members.Append(acceptedInvitation.User) },
        UserLeavedGroup userLeaved => view with { Members = view.Members.Where(member => member != userLeaved.Member) },
        PaymentMade payment => view with
        {
            CreatedConcepts = view.CreatedConcepts.Append(payment.Concept),
            TotalMoneySpent = view.TotalMoneySpent + payment.Amount
        },
        _ => view
    };
    
    public Result<GroupResumeView> IsUserAMember(User user) => Members.Contains(user)
        ? Result<GroupResumeView>.Ok(this) 
        : Result<GroupResumeView>.Ko(new Unauthorized()); 
}
