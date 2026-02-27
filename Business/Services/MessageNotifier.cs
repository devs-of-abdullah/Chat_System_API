using Business.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Business.Hubs;
namespace Business.Services
{
    public class MessageNotifier : IMessageNotifier
    {
        readonly IHubContext<ChatHub> _hub;
        public MessageNotifier(IHubContext<ChatHub> hub)
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
