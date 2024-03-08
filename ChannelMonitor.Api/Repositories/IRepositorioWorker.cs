using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioWorker
    {
        Task<Worker?> GetAll();
    }
}