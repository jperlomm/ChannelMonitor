namespace ChannelMonitor.Api.Entities
{
    public class ChannelDetails
    {
        public int Id { get; set; }
        public int? IdChannel { get; set; }
        public int? PidAudio { get; set; }
        public int? PidVideo { get; set; }
        public ChannelOrigin? ChannelOrigin { get; set; }

    }

    public enum ChannelOrigin
    {
        Satelital,
        Internet
    }

}
