using BankUp.Backend.Groups.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BankUp.Backend.Groups.RenameGroup;

public class Request : RequestWithAuthUser
{
    [FromRoute]
    public Guid GroupId { get; set; }
    
    public string GroupName { get; set; }
}