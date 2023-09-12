using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.Application.DTOs.NotificationDTOs
{
    public class CreateNotificationDTO
    {
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; } 

    }
}
