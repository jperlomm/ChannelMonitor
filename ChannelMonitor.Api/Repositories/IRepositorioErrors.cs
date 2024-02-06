using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioErrors
    {
        Task Create(Error error);
    }
}