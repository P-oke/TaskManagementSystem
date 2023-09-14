using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Enum;
using TaskManagementSystem.Domain.Entities;
using static TaskManagementSystem.Application.Models.Constant;

namespace TaskManagementSystem.Infrastructure.Context.Mappings.UserMaps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public PasswordHasher<User> Hasher { get; set; } = new PasswordHasher<User>();

        public void Configure(EntityTypeBuilder<User> builder)
        {
            var users = new List<User>
            {               
                new User
                {
                    FirstName = "System",
                    LastName = "Super Admin",
                    Id = SystemDetails.SuperAdminId,
                    Email = SystemDetails.SuperAdminEmail,
                    EmailConfirmed = true,
                    NormalizedEmail = SystemDetails.SuperAdminEmail.ToUpper(),
                    PhoneNumber = SystemDetails.Mobile,
                    UserName = SystemDetails.SuperAdminEmail,
                    NormalizedUserName = SystemDetails.SuperAdminEmail.ToUpper(),
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    PasswordHash = Hasher.HashPassword(null, "Dev@1234"),
                    SecurityStamp = "016020e3-5c50-40b4-9e66-bba56c9f5bf2",
                    UserType = UserType.SuperAdmin
                },

            };

            builder.HasData(users);
        }
    }
    
}
