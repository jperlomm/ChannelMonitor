using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Services
{
    public interface IUpdateEntitySignalR
    {
        Task SendUpdateEntity();
        Task SendUpdateWorkerStatus(Worker worker);
    }
}