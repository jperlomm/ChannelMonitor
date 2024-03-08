namespace ChannelMonitor.Api.Entities
{
    public class ContactsTenant
    {
        public int Id { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int MessageProviderId { get; set; }
        public MessageProvider MessageProvider { get; set; } = null!;
        public string Number { get; set; } = null!;
    }
}
