using ChannelMonitor.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChannelMonitor.Api.Services
{
    public interface IUsersServices
    {
        Task<ApplicationUser?> GetUser();
    }
}