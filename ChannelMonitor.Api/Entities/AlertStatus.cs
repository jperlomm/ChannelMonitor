namespace ChannelMonitor.Api.Entities
{
    public class AlertStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null! ; // "ok", "alert", "fail", 
        public string? Color { get; set; }
        public string? Emoji { get; set; }
    }
}
