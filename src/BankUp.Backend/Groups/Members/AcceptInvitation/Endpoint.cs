using BankUp.Backend.Groups.Infrastructure;
using BankUp.Backend.Infrastructure;
using FastEndpoints;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.Members.AcceptInvitation;

[HttpPost($"{ApiUris.Groups}/invitation")]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IInvitationService _invitationService;

    public Endpoint(IGroupRepository groupRepository, IInvitationService invitationService) =>
        (_groupRepository, _invitationService) = (groupRepository, invitationService);

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = request.GetUser();
        var invitationResult = _invitationService.ValidateInvitation(request.InvitationToken, user);
        
        throw new NotImplementedException();
    }
}