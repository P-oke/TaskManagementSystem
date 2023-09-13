using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.DTOs.NotificationDTOs
{
    public class NotificationDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string NotificationType { get; set; }
        public DateTime CreatedOn { get; set; }

        public static implicit operator NotificationDTO(Notification model)
        {
            return model == null ? null : new NotificationDTO
            {
                Id = model.Id,  
                Message = model.Message,
                IsRead = model.IsRead,
                NotificationType = model.NotificationType.GetDescription(),
                CreatedOn = model.CreatedOn,
            };
        }

    }
}
