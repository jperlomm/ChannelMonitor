namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioAlertStatus
    {
       Task<bool> Exist(int id);
    }
}
