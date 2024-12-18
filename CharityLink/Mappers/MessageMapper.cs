using CharityLink.Dtos.Messages;
using CharityLink.Models;

namespace CharityLink.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto ToMessageDto(this Message message)
        {
            return new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead,
                ImageUrl = message.ImageUrl,
            };
        }
    }
}
