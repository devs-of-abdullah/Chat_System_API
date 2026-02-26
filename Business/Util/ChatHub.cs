using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Business
{
    public class ChatHub : Hub
    {
        static readonly ConcurrentDictionary<int, string> Connections = new();

        public override Task OnConnectedAsync()
        {
            var userIdString = Context.GetHttpContext()!.Request.Query["userId"].ToString();

            if (!int.TryParse(userIdString, out var userId))
            {
                throw new HubException("Invalid or missing userId query parameter.");
            }

            Connections[userId] = Context.ConnectionId;

            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var item = Connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
            if (item.Key != 0)
                Connections.TryRemove(item.Key, out _);

            return base.OnDisconnectedAsync(exception);
        }

        public static bool TryGetConnection(int userId, out string connectionId)
            => Connections.TryGetValue(userId, out connectionId!);
    }
}
