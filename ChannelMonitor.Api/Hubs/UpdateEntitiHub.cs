using Microsoft.AspNetCore.SignalR;

namespace ChannelMonitor.Api.Hub
{
    public class UpdateEntitiHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IHubContext<UpdateEntitiHub> _hubContext;

        public UpdateEntitiHub(IHubContext<UpdateEntitiHub> hubContext)
        {
            _hubContext = hubContext;
        }

    }

}
