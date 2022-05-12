using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Monads;

namespace BankUp.Backend.Groups.GetResume;

[HttpGet($"{ApiUris.Groups}/{{GroupId}}")]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    public Endpoint(IGroupRepository groupRepository) => _groupRepository = groupRepository;
    
    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var maybeGroup = await _groupRepository.Read(request.GroupId);
        var resultView = maybeGroup.ToResult()
            .Map(group => group.IsUserAMember(request.UserId))
            .UnWrap()
            .Map(group => GroupResumeView.BuildView(group.Events));
        await HandleResult(resultView);
    }
    
    private Task HandleResult(Result<GroupResumeView> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(GroupResumeView view) => SendOkAsync(new OkResponse{ Group = view });
    private Task OnKoResult(Error error) => SendAsync(new KoResponse { Error = error.ToString() }, 500);
}