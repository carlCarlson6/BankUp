using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Monads;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.RenameGroup;

[HttpPut($"{ApiUris.Groups}/{{GroupId}}/rename")]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    public Endpoint(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var maybeGroup = await _groupRepository.Read(request.GroupId);
        var resultGroup =  maybeGroup.ToResult()
            .Map(group => group.IsUserAMember(request.UserId))
            .UnWrap()
            .Map(group => GroupRenamed.Create(request.GroupName).Map(group.Add))
            .UnWrap();
        var storeResult = (await resultGroup.Map(_groupRepository.Store)).UnWrap();
        await HandleResult(storeResult);
    }

    private Task HandleResult(Result<Group> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(Group group) => SendOkAsync(new OkResponse{ Group = GroupResumeView.BuildView(group.Events) });
    private Task OnKoResult(Error error) => SendAsync(new KoResponse { Error = error.ToString() }, 500);
}