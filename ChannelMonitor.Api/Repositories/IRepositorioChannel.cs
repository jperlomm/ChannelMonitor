using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioChannel
    {
        Task<int> Create(Channel channel);
        Task Update(Channel channel);
        Task<List<Channel>> GetAll();
        Task<Channel?> GetById(int id);

    }
}
