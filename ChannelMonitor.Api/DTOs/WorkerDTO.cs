namespace ChannelMonitor.Api.DTOs
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string? Ip { get; set; }
        public string? Port { get; set; }
        public string? Status { get; set; }
    }
}
