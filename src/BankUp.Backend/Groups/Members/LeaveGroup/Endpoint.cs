using BankUp.Backend.Groups.Infrastructure;
using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Monads;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.Members.LeaveGroup;

// TODO - TEST
[HttpDelete($"{ApiUris.Groups}/{{GroupId}}")]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    public Endpoint(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var user = request.GetUser();
        var resultGroup = (await _groupRepository.Read(request.GroupId))
            .ToResult()
            .Map(group => group.IsUserAMember(user))
            .UnWrap()
            .Map(group => group.Add(UserLeavedGroup.Create(user)));
        var storeResult = (await resultGroup.Map(_groupRepository.Store))
            .UnWrap();
        await storeResult.Map(NotifyEventToOtherMember);
        await HandleResult(storeResult);
    }

    private static Task<Result<Group>> NotifyEventToOtherMember(Group group) => throw new NotImplementedException();
    private Task HandleResult(Result<Group> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(Group group) => SendAsync(new Response(), 204);
    private Task OnKoResult(Error error) => SendAsync(new KoResponse { Error = error.ToString() }, 500);
}