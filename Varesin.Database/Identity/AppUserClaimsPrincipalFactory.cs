using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.Enumeration;

namespace Varesin.Database.Identity
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;
        public AppUserClaimsPrincipalFactory(UserManager<User> userManager,
            Microsoft.Extensions.Options.IOptions<IdentityOptions> options) : base(userManager, options)
        {
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Surname,user.FullName),
                new Claim(ClaimTypes.Thumbprint,user.ImageName ?? "")
            });

            if (user.IsSuperAdmin)
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, AccessCode.FullAccess.ToString()));

            else
            {
                foreach (var role in roles)
                    ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return principal;
        }
    }
}
