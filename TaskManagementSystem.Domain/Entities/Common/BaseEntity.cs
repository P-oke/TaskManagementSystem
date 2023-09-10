using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public T Id { get; set; }
        public Guid CreatorUserId { get; set; } 
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
