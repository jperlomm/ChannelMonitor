using Microsoft.AspNetCore.Identity;

namespace ChannelMonitor.Api.Services
{
    public interface IUsersServices
    {
        Task<IdentityUser?> GetUser();
    }
}