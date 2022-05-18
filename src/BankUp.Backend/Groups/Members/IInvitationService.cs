using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Monads;
using static System.String;

namespace BankUp.Backend.Groups.Members;

public interface IInvitationService
{
    Result<Invitation> ValidateInvitation(string invitationToken, User user);
}

public class InvitationService : IInvitationService
{
    public Result<Invitation> ValidateInvitation(string invitationToken, User user) =>
        ReadClaim(invitationToken, "TODO", "TODO", "TODO")
            .Map(ReadInvitation)
            .Map(result => ValidateInvitation(result, user));

    private static Result<ClaimsPrincipal> ReadClaim(string invitationToken, string secretKey, string audienceToken, string issuerToken)
    {
        var validationParameters = new TokenValidationParameters()
        {
            ValidAudience = audienceToken,
            ValidIssuer = issuerToken,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey))
        };
        
        var handler = new JwtSecurityTokenHandler();

        if (handler.CanReadToken(invitationToken))
            return InvalidInvitation.AsResult<ClaimsPrincipal>();

        try
        {
            var claims = handler.ValidateToken(invitationToken, validationParameters, out _);
            return Result<ClaimsPrincipal>.Ok(claims);
        }
        catch
        {
            return InvalidInvitation.AsResult<ClaimsPrincipal>();
        }
    }
    
    private static Result<Invitation> ReadInvitation(Result<ClaimsPrincipal> claimsResult) =>
        claimsResult.Map(
            claim =>
            {
                var group = claim.Claims.FirstOrDefault(c => c.Type == nameof(Invitation.ForGroup))?.Value;
                var userEmail = claim.Claims.FirstOrDefault(c => c.Type == nameof(Invitation.ForUser))?.Value;
                var date = claim.Claims.FirstOrDefault(c => c.Type == nameof(Invitation.ValidTill))?.Value;
                
                if (IsNullOrWhiteSpace(group) || IsNullOrWhiteSpace(userEmail) || IsNullOrWhiteSpace(date) || Guid.TryParse(group, out var forGroup) || DateTime.TryParse(date, out var validTill))
                    return InvalidInvitation.AsResult<Invitation>();
                
                return Result<Invitation>.Ok(new Invitation(forGroup, userEmail, validTill));
            },
            Result<Invitation>.Ko);

    private static Result<Invitation> ValidateInvitation(Result<Invitation> invitationResult, User user) =>
        invitationResult.Map(
            invitation => invitation.ForUser == user.Email || invitation.ValidTill < DateTime.UtcNow
                ? InvalidInvitation.AsResult<Invitation>()
                : Result<Invitation>.Ok(invitation),
            Result<Invitation>.Ko);
}