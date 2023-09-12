using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.DTOs.NotificationDTOs
{
    public class NotificationDTO
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }

        public static implicit operator NotificationDTO(Notification model)
        {
            return model == null ? null : new NotificationDTO
            {
                Message = model.Message,
                IsRead = model.IsRead
            };
        }

    }
}
