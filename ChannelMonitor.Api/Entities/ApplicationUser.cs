using Microsoft.AspNetCore.Identity;

namespace ChannelMonitor.Api.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
