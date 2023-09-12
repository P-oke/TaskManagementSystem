using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }  
        public DbSet<TaskManagementSystem.Domain.Entities.Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Notification> Notifications { get; set; } 

    }
}
