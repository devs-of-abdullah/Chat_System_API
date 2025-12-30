

using API;
using Business.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Business.Services
{
    public class SignalRMessageNotifier : IMessageNotifier
    {
        readonly IHubContext<ChatHub> _hub;
        public SignalRMessageNotifier(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }
        public async Task NotifyAsync(int userId, object payload)
        {
            if (ChatHub.TryGetConnection(userId, out var connId))
            {
                await _hub.Clients.Client(connId).SendAsync("ReceiveMessage", payload);
            }
        }
    }
}
