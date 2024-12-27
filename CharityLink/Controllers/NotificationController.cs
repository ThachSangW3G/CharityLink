using CharityLink.Dtos.Messages;
using CharityLink.Dtos.Notifications;
using CharityLink.Hubs;
using CharityLink.Interfaces;
using CharityLink.Mappers;
using CharityLink.Models;
using CharityLink.Repositories;
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
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepository;

        public NotificationController(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext, IUserRepository userRepository, IConfiguration configuration, IPostRepository postRepository)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _userRepository = userRepository;
            _configuration = configuration;
            _postRepository = postRepository;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsForUserAsync(userId);


            var notificationDtos = notifications.Select(n => n.ToNotificationDto());

            var updatedNotificationDtos = new List<NotificationDto>();
            var baseUrl = _configuration["NgrokBaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";

            foreach (var notificationDto in notificationDtos)
            {
                if (notificationDto.Type == "POST")
                {
                    
                    var user = await _userRepository.GetByIdAsync(notificationDto.SenderId);
                    notificationDto.AvatarUrl = $"{baseUrl}{user.AvatarUrl}";
                    notificationDto.UserName = user.Name;

                    var post = await _postRepository.GetByIdAsync(notificationDto.PostId);
                    notificationDto.ImageUrl = $"{baseUrl}{post.ImageUrl}";
                }

                updatedNotificationDtos.Add(notificationDto);
            }


            return Ok(updatedNotificationDtos);
        }

        //[HttpPost]
        //public async Task<IActionResult> SendNotification([FromBody] CreateNotificationDto dto)
        //{
        //    var notification = dto.ToNotificationFromCreateDto();

        //    // Lưu thông báo vào database
        //    await _notificationRepository.CreateNotificationAsync(notification);

        //    // Gửi thông báo thời gian thực qua SignalR
        //    await _hubContext.Clients.Group(dto.UserId.ToString())
        //        .SendAsync("ReceiveNotification", dto.Content);

        //    return Ok();
        //}


        [HttpPatch("{notificationId:int}/read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] CreateNotificationDto dto)
        {
            // Tạo message mới
            var notification = dto.ToNotificationFromCreateDto();

            // Lưu message vào cơ sở dữ liệu
            await _notificationRepository.CreateNotificationAsync(notification);

            // Gửi sự kiện SignalR đến client
            await _hubContext.Clients.All
                .SendAsync("ReceiveNotification", notification.NotificationId, notification.UserId);

            return Ok(notification);
        }
    }
}
