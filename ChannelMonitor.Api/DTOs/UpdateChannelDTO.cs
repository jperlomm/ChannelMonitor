using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.DTOs
{
    public class UpdateChannelDTO
    {
        public string? Name { get; set; }
        public int? Number { get; set; }
        public string? Ip { get; set; }
        public string? Port { get; set; }
        public bool? InProcessing { get; set; }
        public bool? ShouldMonitorVideo { get; set; }
        public bool? ShouldMonitorAudio { get; set; }
        public int? AudioThreshold { get; set; }
        public int? VideoFilterLevel { get; set; }
        public TimeSpan? MonitoringStartTime { get; set; }
        public TimeSpan? MonitoringEndTime { get; set; }

        public int? VideoFailureId { get; set; }
        public AlertStatus? VideoFailure { get; set; }

        public int? AudioFailureId { get; set; }
        public AlertStatus? AudioFailure { get; set; }

        public int? GeneralFailureId { get; set; }
        public AlertStatus? GeneralFailure { get; set; }

        public int? ChannelDetailsId { get; set; }
        public ChannelDetail? ChannelDetails { get; set; }

        public List<FailureLogging>? FailureLogging { get; set; } = new List<FailureLogging>();
        public DateTime? LastScan { get; set; }
        public double? LastVolume { get; set; }
        public int? IdChannelBackUp { get; set; }
    }
}
