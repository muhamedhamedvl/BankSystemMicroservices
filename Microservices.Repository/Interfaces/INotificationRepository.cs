using Microservices.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repository.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsAsync(int userId);
        Task AddNotificationAsync(int userId, string message);
        Task DeleteNotificationAsync(int notificationId);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId);
        Task<int> GetUnreadNotificationCountAsync(int userId);
    }
}
