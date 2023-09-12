using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.Domain.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
