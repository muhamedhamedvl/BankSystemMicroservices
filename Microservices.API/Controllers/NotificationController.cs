using Microservices.Core.Models;
using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _notificationService.GetNotificationsAsync(userId);
            return Ok(notifications);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddNotification(int userId, [FromBody] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Message cannot be empty.");
            }
            await _notificationService.AddNotificationAsync(userId, message);
            return CreatedAtAction(nameof(GetNotifications), new { userId }, null);
        }
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            await _notificationService.DeleteNotificationAsync(notificationId);
            return NoContent();
        }
        [HttpPut("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            await _notificationService.MarkNotificationAsReadAsync(notificationId);
            return NoContent();
        }
        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnreadNotifications(int userId)
        {
            var unreadNotifications = await _notificationService.GetUnreadNotificationsAsync(userId);
            return Ok(unreadNotifications);
        }
    }
}
