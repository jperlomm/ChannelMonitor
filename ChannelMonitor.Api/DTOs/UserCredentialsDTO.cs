using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.DTOs
{
    public class UserCredentialsDTO
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Guid TenantId { get; set; }
    }
}
