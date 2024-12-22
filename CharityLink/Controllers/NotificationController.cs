using CharityLink.Dtos.Notifications;
using CharityLink.Hubs;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsForUserAsync(userId);

            var notificationDtos = notifications.Select(n => n.ToNotificationDto());
            return Ok(notificationDtos);
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] CreateNotificationDto dto)
        {
            var notification = dto.ToNotificationFromCreateDto();

            // Lưu thông báo vào database
            await _notificationRepository.CreateNotificationAsync(notification);

            // Gửi thông báo thời gian thực qua SignalR
            await _hubContext.Clients.Group(dto.UserId.ToString())
                .SendAsync("ReceiveNotification", dto.Content);

            return Ok();
        }


        [HttpPatch("{notificationId:int}/read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
            return NoContent();
        }
    }
}
