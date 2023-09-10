using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        public string Message { get; set; }
        public bool IsRead { get; set; } 
    }
}
