using CharityLink.Dtos.Communities;
using CharityLink.Dtos.Messages;
using CharityLink.Hubs;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
using CharityLink.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IConfiguration _configuration;

        public MessageController(IMessageRepository messageRepository, IHubContext<ChatHub> hubContext, IConfiguration configration)
        {
            _messageRepository = messageRepository;
            _hubContext = hubContext;
          
            _configuration = configration;
        }

        [HttpGet("chat/{userId1}/{userId2}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId1, userId2);

            var messagesDtos = messages.Select(m => m.ToMessageDto());

            var updatedMessagesDtoList = new List<MessageDto>();

            foreach (var dto in messagesDtos)
            {
                dto.UnreadCount = 0;
                dto.UserName = "";       
                dto.AvatarUrl = "";
                updatedMessagesDtoList.Add(dto);
            }

            return Ok(updatedMessagesDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
        {
            // Tạo message mới
            var message = new Message
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            // Lưu message vào cơ sở dữ liệu
            await _messageRepository.CreateMessageAsync(message);

            // Gửi sự kiện SignalR đến client
            await _hubContext.Clients.All
                .SendAsync("ReceiveMessage", dto.SenderId, dto.ReceiverId, dto.Content, message.SentAt);

            return Ok(message);
        }


        [HttpGet("get-latest-messages/{IdUser:int}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetLatestMessagesForUser([FromRoute] int IdUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var messages = await _messageRepository.GetLatestMessagesForUser(IdUser);
           

            var messagesDtos = messages.Select(m => m.ToMessageDto());

            var updatedMessagesDtoList = new List<MessageDto>();

            foreach (var dto in messagesDtos)
            {
                int otherUserId = dto.SenderId == IdUser ? dto.ReceiverId : dto.SenderId;

                dto.UnreadCount = await _messageRepository.CountUnreadMessages(IdUser, otherUserId);

                var user = await _messageRepository.GetUserByIdUser(otherUserId);

                if (user != null)
                {
                    dto.UserName = user.Name;
                    var ngrokUrl = _configuration.GetValue<string>("NgrokBaseUrl");
                    dto.AvatarUrl = $"{ngrokUrl}{user.AvatarUrl}";
                }

                updatedMessagesDtoList.Add(dto);
            }

            return Ok(updatedMessagesDtoList);

        }


        [HttpPost]
        [Route("mark-messages-is-read/{IdUser:int}-{OtherIdUser}")]
        public async Task<IActionResult> MarkMessgeAsRead([FromRoute] int IdUser, int OtherIdUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (IdUser == OtherIdUser) return BadRequest("Không được trùng ID");

            await _messageRepository.MarkMessagesAsRead(IdUser, OtherIdUser);

            return Ok();

        }
    }
}
