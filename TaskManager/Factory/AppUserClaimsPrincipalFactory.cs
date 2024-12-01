using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Controllers;
using TaskManager.Models;

namespace TaskManager.Factory
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email ?? string.Empty));
            identity.AddClaim(new Claim("FullName", $"{user.FirstName} {user.LastName}"));
            identity.AddClaim(new Claim("Email", user.Email));

           
            return identity;
        }
    }
}
