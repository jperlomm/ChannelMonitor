using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioContactsTenant
    {
        Task<List<ContactsTenant>> GetAll();
    }
}
