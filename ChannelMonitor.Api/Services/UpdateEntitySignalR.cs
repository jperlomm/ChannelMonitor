using AutoMapper;
using ChannelMonitor.Api.DTOs;
using ChannelMonitor.Api.Hub;
using ChannelMonitor.Api.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace ChannelMonitor.Api.Services
{
    public class UpdateEntitySignalR : IUpdateEntitySignalR
    {
        private readonly IHubContext<UpdateEntitiHub> _hubContext;
        private readonly IMapper _mapper;
        private readonly IRepositorioChannel _repositorio;
        private readonly ITenantProvider _tenantProvider;

        public UpdateEntitySignalR(IHubContext<UpdateEntitiHub> hubContext, IMapper mapper, IRepositorioChannel repositorio, ITenantProvider tenantProvider)
        {
            _hubContext = hubContext;
            _mapper = mapper;
            _repositorio = repositorio;
            _tenantProvider = tenantProvider;
        }

        public async Task SendUpdateEntity()
        {
            var channels = await _repositorio.GetAlarmedChannels();
            var channelsDTO = _mapper.Map<List<ChannelDTO>>(channels);
            //await _hubContext.Clients.All.SendAsync("updatechannel", channelsDTO);
            await _hubContext.Clients.Group(_tenantProvider.GetTenantId().ToString()).SendAsync("updatechannel", channelsDTO);
        }
    }

}
