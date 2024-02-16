using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.DTOs
{
    public class FailureLoggingDTO
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public int? FailureTypeId { get; set; }
        public string? Url { get; set; }
        public DateTime? DateFailure { get; set; }
        public string? Detail { get; set; }
    }
}
