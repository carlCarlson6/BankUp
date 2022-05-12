using BankUp.Backend.Infrastructure;
using FastEndpoints;
using Monads;

// ReSharper disable once UnusedType.Global
namespace BankUp.Backend.Groups.CreateGroup;

[HttpPut(ApiUris.Groups)]
public class Endpoint : Endpoint<Request, Response>
{
    private readonly IGroupRepository _groupRepository;
    public Endpoint(IGroupRepository groupRepository) => _groupRepository = groupRepository;

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var createEventResult = (await GroupCreated
            .Create(request.GroupName, new List<Guid> { request.Creator })
            .Map(@event => _groupRepository.Store(new Group(@event))))
            .UnWrap();
        await HandleResult(createEventResult);
    }

    private Task HandleResult(Result<Group> result) => result.Map(OnOkResult, OnKoResult);
    private Task OnOkResult(Group group) => SendAsync(new OkResponse { GroupId = group.Id }, 201);
    private Task OnKoResult(Error error) => SendAsync(new KoResponse{ Error = error.ToString() }, 500);
}