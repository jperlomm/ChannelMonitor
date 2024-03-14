using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioWorker
    {
        Task<Worker?> GetAll();
        Task Update(Worker worker);
        Task<Worker?> GetById(int id);
    }
}