using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.Domain.Entities
{
    public class Task : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public Project Project { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
