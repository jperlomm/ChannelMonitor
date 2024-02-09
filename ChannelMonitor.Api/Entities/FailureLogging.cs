namespace ChannelMonitor.Api.Entities
{
    public class FailureLogging
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public int? FailureTypeId { get; set; }
        public string? Url { get; set; }
        public DateTime? DateFailure { get; set; }
    }

}