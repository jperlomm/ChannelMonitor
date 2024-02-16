using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioFailureLogging
    {
        Task<int> Create(FailureLogging failureLogging);

        Task<List<FailureLogging>> GetAll(int channelId);

    }
}