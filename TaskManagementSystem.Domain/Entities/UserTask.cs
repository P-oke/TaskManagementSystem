using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities
{
    public class UserTask : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TaskId { get; set; }
        public Task Task { get; set; }

    }
}
