using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.AspNetCore.SignalR;

namespace CharityLink.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = DateTime.UtcNow
            };


            // Lưu tin nhắn vào database
            await _messageRepository.CreateMessageAsync(message);

            // Gửi tin nhắn đến người nhận
            await Clients.All
                .SendAsync("ReceiveMessage", senderId, receiverId, content, message.SentAt);
        }


        public async Task SendMessageWithImage(int senderId, int receiverId, string base64Image)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = "",
                SentAt = DateTime.UtcNow
            };

            string imageUrl = string.Empty;
            if (!string.IsNullOrEmpty(base64Image))
            {
                var uploadsFolder = Path.Combine("wwwroot", "messages");
                Directory.CreateDirectory(uploadsFolder);
                var uniqueFileName = Guid.NewGuid().ToString();
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                var imageBytes = Convert.FromBase64String(base64Image);
                await File.WriteAllBytesAsync(filePath, imageBytes);

                imageUrl = $"/messages/{uniqueFileName}";
            }

            message.ImageUrl = imageUrl;

            // Lưu tin nhắn vào database
            await _messageRepository.CreateMessageAsync(message);

            // Gửi tin nhắn đến người nhận
            await Clients.All
                .SendAsync("ReceiveMessage", senderId, receiverId, "", message.SentAt);
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
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
