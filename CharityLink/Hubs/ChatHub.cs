using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.AspNetCore.SignalR;

namespace CharityLink.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            var messageRepository = Context.GetHttpContext().RequestServices.GetService<IMessageRepository>();
            await messageRepository.CreateMessageAsync(message);

            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, content, message.SentAt);
        }

        public override Task OnConnectedAsync()
        {
            // Map connectionId với userId để gửi tin nhắn trực tiếp
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                // Dùng UserId làm Group cho SignalR
                Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
