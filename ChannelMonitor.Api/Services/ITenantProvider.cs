namespace ChannelMonitor.Api.Services
{
    public interface ITenantProvider
    {
        Guid? GetTenantId();
    }
}