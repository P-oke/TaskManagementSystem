using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public bool IsActive { get; set; }
        public Guid? CreatorUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool IsDefaultRole { get; set; }
    }
}
