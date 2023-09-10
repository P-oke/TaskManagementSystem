using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
