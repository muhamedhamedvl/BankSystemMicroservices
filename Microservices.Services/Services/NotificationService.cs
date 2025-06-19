using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Services.Interfaces;
using Microservices.Core.Models;
using Microservices.Repository.Interfaces;
namespace Microservices.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<Notification>> GetNotificationsAsync(int userId)
        {
            return await _notificationRepository.GetNotificationsAsync(userId);
        }
        public async Task AddNotificationAsync(int userId, string message)
        {
            await _notificationRepository.AddNotificationAsync(userId, message);
        }
        public async Task DeleteNotificationAsync(int notificationId)
        {
            await _notificationRepository.DeleteNotificationAsync(notificationId);
        }
        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkNotificationAsReadAsync(notificationId);
        }
        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId)
        {
            return await _notificationRepository.GetUnreadNotificationsAsync(userId);
        }
        public async Task<int> GetUnreadNotificationCountAsync(int userId)
        {
            return await _notificationRepository.GetUnreadNotificationCountAsync(userId);
        }
    }
}
