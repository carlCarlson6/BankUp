using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace BankUp.Backend.Groups.GetResume;

public class Request
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId { get; set; }
    
    [FromRoute]
    public Guid GroupId { get; set; }
}