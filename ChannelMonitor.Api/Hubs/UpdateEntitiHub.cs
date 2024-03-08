using ChannelMonitor.Api.Services;
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

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

    }

}
