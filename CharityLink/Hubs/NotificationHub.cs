using Microsoft.AspNetCore.SignalR;

namespace CharityLink.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                // Thêm ConnectionId vào Group để gửi thông báo tới đúng người
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification(int userId, string message)
        {
            // Gửi thông báo thời gian thực tới client
            await Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", message);
        }
    }
}
