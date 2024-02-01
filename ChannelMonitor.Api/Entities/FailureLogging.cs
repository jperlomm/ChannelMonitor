namespace ChannelMonitor.Api.Entities
{
    public class FailureLogging
    {
        public int Id { get; set; }
        public int IdChannel { get; set; }
        public FailureType FailureType { get; set; } = null!;
        public string? Url { get; set; }
        public DateTime? DateFailure { get; set; }
    }

}