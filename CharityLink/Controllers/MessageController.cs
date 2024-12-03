using CharityLink.Dtos.Messages;
using CharityLink.Interfaces;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }


        [HttpGet("chat/{userId1}/{userId2}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId1, userId2);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
        {
            var message = new Message
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.CreateMessageAsync(message);

            return Ok(message);
        }
    }
}
