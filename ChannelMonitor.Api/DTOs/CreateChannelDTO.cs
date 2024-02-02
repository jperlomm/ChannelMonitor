
namespace ChannelMonitor.Api.DTOs
{
    public class CreateChannelDTO
    {
        public string Name { get; set; } = null!;
        public int Number { get; set; }
        public string Ip { get; set; } = null!;
        public string Port { get; set; } = null!;
        public bool? ShouldMonitorVideo { get; set; }
        public bool? ShouldMonitorAudio { get; set; }
        public int? AudioThreshold { get; set; }
        public int? VideoFilterLevel { get; set; }
        public TimeSpan? MonitoringStartTime { get; set; }
        public TimeSpan? MonitoringEndTime { get; set; }
        public int? IdChannelBackUp { get; set; }
    }
}
