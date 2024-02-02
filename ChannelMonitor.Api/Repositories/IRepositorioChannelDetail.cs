namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioChannelDetail
    {
        Task<bool> Exist(int id);
    }
}
