using AutoMapper;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Repositories;

namespace ChannelMonitor.Api.Services
{
    public interface IUpdateEntitySignalR
    {
        Task SendUpdateEntity(IRepositorioChannel repositorio, IMapper mapper, Channel channel);
    }
}