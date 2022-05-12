using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BankUp.Backend.Groups.RenameGroup;

public class Request
{
    [FromRoute]
    public Guid GroupId { get; set; }
    
    public string GroupName { get; set; }
    
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
}