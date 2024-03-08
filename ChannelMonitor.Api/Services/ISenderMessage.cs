namespace ChannelMonitor.Api.Services
{
    public interface ISenderMessage
    {
        Task SendMessage(string content, string destination);
    }
}
