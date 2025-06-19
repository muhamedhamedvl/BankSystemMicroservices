using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; } = null!;
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum NotificationType
    {
        Email,
        SMS
    }
}
