using BankUp.Backend.Groups.Infrastructure;

namespace BankUp.Backend.Groups.Members.AcceptInvitation;

public class Request : RequestWithAuthUser
{
    public string InvitationToken { get; set; }
}