using Microsoft.AspNetCore.Identity;

namespace ChannelMonitor.Api.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public UsersServices(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        // Recuperamos informacion del usuario logeado segun que hayamos guardado en los clains
        // (UsersEndpoints -> CreateToken)
        public async Task<IdentityUser?> GetUser()
        {
            var emailClaim = httpContextAccessor.HttpContext!
                .User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim is null) return null;

            var email = emailClaim.Value;
            return await userManager.FindByEmailAsync(email);
        }

    }
}
