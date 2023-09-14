using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using static TaskManagementSystem.Application.Models.Constant;

namespace TaskManagementSystem.Infrastructure.Context.Mappings.UserMaps
{
    internal class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var roles = new Role[]
            {
                new Role
                {
                    Id = AppRole.SuperAdminRoleId,
                    Name = AppRole.SUPERADMIN.ToString(),
                    NormalizedName = AppRole.SUPERADMIN.ToString(),
                },
            };

            builder.HasData(roles);
        }   
    }
}
