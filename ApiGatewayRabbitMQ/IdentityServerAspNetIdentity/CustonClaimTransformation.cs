using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace IdentityServerAspNetIdentity
{
    public class CustonClaimTransformation : IClaimsTransformation 
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "location";
            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                claimsIdentity.AddClaim(new Claim(claimType, "location"));
            }

           // principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
