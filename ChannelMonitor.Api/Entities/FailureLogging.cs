namespace ChannelMonitor.Api.Entities
{
    public class FailureLogging
    {
        public int Id { get; set; }
        public int IdChannel { get; set; }
        public FailureType FailureType { get; set; }
        public string? Url { get; set; }
        public DateTime? DateFailure { get; set; }
    }

    public enum FailureType
    {
        General,
        Audio,
        Video
    }

}
