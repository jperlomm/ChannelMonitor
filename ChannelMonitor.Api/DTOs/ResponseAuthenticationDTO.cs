namespace ChannelMonitor.Api.DTOs
{
    public class ResponseAuthenticationDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
