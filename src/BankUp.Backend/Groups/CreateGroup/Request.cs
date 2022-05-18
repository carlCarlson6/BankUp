using BankUp.Backend.Groups.Infrastructure;

namespace BankUp.Backend.Groups.CreateGroup;

public class Request : RequestWithAuthUser
{
    public string GroupName { get; set; }
}