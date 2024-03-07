namespace ChannelMonitor.Api.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string? Ip { get; set; }
        public string? Port { get; set; }
        public Guid TenantId { get; set; }
        public string? Status { get; set;}
    }
}
