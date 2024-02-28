namespace ChannelMonitor.Api.Entities
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Number { get; set; }
        public string Ip { get; set; } = null!;
        public string Port { get; set; } = null!;
        public bool InProcessing { get; set; }
        public bool ShouldMonitorVideo { get; set; } = true;
        public bool ShouldMonitorAudio { get; set; } = true;
        public int AudioThreshold { get; set; } = -80;
        public int VideoFilterLevel { get; set; } = 1;
        public TimeSpan? MonitoringStartTime { get; set; }
        public TimeSpan? MonitoringEndTime { get; set; }

        public int? VideoFailureId { get; set; } = 4;
        public AlertStatus VideoFailure { get; set; } = null!;

        public int? AudioFailureId { get; set; } = 4;
        public AlertStatus AudioFailure { get; set; } = null!;

        public int? GeneralFailureId { get; set; } = 4;
        public AlertStatus GeneralFailure { get; set; } = null!;

        public int? ChannelDetailsId { get; set; }
        public ChannelDetail? ChannelDetails { get; set; }

        public List<FailureLogging> FailureLogging { get; set; } = new List<FailureLogging>();
        public DateTime? LastScan { get; set; }
        public double? LastVolume { get; set; }
        public int? IdChannelBackUp { get; set; }
        public Guid? TenantId { get; set; }

    }
}
