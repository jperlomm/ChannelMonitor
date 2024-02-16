using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.DTOs
{
    public class CreateFailureLoggingDTO
    {
        public int ChannelId { get; set; }
        public int? FailureTypeId { get; set; }
        public IFormFile? File { get; set; }
        public string? Detail { get; set; }
    }
}
