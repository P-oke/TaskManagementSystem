using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Models
{
    public class Constant
    {
        public class SystemDetails
        {
            public const string SuperAdminEmail = "superadmin@tms.com";
            public static readonly Guid SuperAdminId = Guid.Parse("3fb897c8-c25d-4328-9813-cb1544369fba");

            public const string Mobile = "07060882817";

        }

        public class AppRole
        {
            public static Guid SuperAdminRoleId => Guid.Parse("131e8b31-59a7-4c80-9b71-08b60e951e5c");
            public const string SUPERADMIN = nameof(SUPERADMIN);
           
        }

    }
}
