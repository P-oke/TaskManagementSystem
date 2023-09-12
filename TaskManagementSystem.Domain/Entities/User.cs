using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreationTime { get; set; }
        public List<UserTask> UserTasks { get; set; } = new List<UserTask>();

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }

    public class UserClaim : IdentityUserClaim<Guid> { }

    public class UserRole : IdentityUserRole<Guid> { }

    public class UserLogin : IdentityUserLogin<Guid>
    {
    }

    public class RoleClaim : IdentityRoleClaim<Guid> { }

    public class UserToken : IdentityUserToken<Guid> { }

  
}
