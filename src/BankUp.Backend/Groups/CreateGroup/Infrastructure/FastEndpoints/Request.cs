using BankUp.Backend.Groups.Infrastructure;

namespace BankUp.Backend.Groups.CreateGroup.Infrastructure.FastEndpoints;

public class Request : RequestWithAuthUser
{
    public string GroupName { get; set; }
    public List<string> Members { get; set; }
}