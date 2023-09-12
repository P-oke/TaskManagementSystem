using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.DTOs.NotificationDTOs
{
    public class MarkAsReadDTO
    {
        public Guid NotificationId { get; set; }
        public bool MarkAsRead { get; set; } 
    }
}
