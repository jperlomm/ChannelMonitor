using AutoMapper;
using ChannelMonitor.Api.Entities;

namespace ChannelMonitor.Api.Repositories
{
    public interface IRepositorioUpdateEntity
    {
        Task SendUpdateEntity(IRepositorioChannel repositorio, IMapper mapper, Channel channel);
    }
}