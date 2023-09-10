using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities
{
    public class Project : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
