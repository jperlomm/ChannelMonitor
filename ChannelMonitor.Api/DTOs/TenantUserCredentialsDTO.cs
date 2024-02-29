namespace ChannelMonitor.Api.DTOs
{
    public class TenantUserCredentialsDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid TenantId { get; set; }
        public bool HasNoEndDate { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
