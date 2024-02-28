namespace ChannelMonitor.Api.Entities
{
    public class ChannelOrigin
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? TenantId { get; set; }
    }
}
