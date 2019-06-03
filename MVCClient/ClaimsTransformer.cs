using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace MVCClient
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IHttpContextAccessor _ctxAccess;
        public ClaimsTransformer(IHttpContextAccessor ctxAccess)
        {
            _ctxAccess = ctxAccess;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity != null && principal.Identity.IsAuthenticated &&  principal.Identity is ClaimsIdentity identity )
            {
                var id = identity.Claims.Single(c => c.Type == "sub").Value;

                if (!identity.Claims.Any(cw => cw.Type == identity.NameClaimType))
                    identity.AddClaim(new Claim(identity.NameClaimType, id));
            }

            return Task.FromResult(principal);
        }
    }
}
