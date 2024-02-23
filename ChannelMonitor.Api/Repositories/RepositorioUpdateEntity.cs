using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Entities;
using ChannelMonitor.Api.Hub;
using Microsoft.AspNetCore.SignalR;

namespace ChannelMonitor.Api.Repositories
{
    public class RepositorioUpdateEntity : IRepositorioUpdateEntity
    {
        private readonly IHubContext<UpdateEntitiHub> _hubContext;

        public RepositorioUpdateEntity(IHubContext<UpdateEntitiHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendUpdateEntity(IRepositorioChannel repositorio, IMapper mapper, Channel channel)
        {
            var channelsDTO = mapper.Map<ChannelDTO>(channel);

            await _hubContext.Clients.All.SendAsync("updatechannel", channelsDTO);
        }
    }

}
