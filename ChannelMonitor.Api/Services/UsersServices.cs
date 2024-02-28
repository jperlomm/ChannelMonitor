using ChannelMonitor.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChannelMonitor.Api.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersServices(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        // Recuperamos informacion del usuario logeado segun que hayamos guardado en los clains
        // (UsersEndpoints -> CreateToken)
        public async Task<ApplicationUser?> GetUser()
        {
            var UserNameClaim = httpContextAccessor.HttpContext!
                .User.Claims.Where(x => x.Type == "tenant_id").FirstOrDefault();

            if (UserNameClaim is null) return null;

            var userName = UserNameClaim.Value;
            return await userManager.FindByNameAsync(userName);
        }

    }
}
