using AutoMapper;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Hub;
using ChannelMonitor.Api.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace ChannelMonitor.Api.Services
{
    public class UpdateEntitySignalR : IUpdateEntitySignalR
    {
        private readonly IHubContext<UpdateEntitiHub> _hubContext;

        public UpdateEntitySignalR(IHubContext<UpdateEntitiHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendUpdateEntity(IRepositorioChannel repositorio, IMapper mapper, Channel channel)
        {
            await _hubContext.Clients.All.SendAsync("updatechannel", channel);
        }
    }

}
