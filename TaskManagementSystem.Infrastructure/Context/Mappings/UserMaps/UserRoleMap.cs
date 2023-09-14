using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using static TaskManagementSystem.Application.Models.Constant;

namespace TaskManagementSystem.Infrastructure.Context.Mappings.UserMaps
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            var userRoles = new List<UserRole>()
            {
                new UserRole
                {
                    UserId = SystemDetails.SuperAdminId,
                    RoleId = AppRole.SuperAdminRoleId
                },
            };

            builder.HasData(userRoles);

        }

    }

}
