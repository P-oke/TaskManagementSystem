using Hangfire;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.UnitTests.UserTests
{
    public class UserServiceFactory
    {
        public readonly ApplicationDbContext Context;

        public UserServiceFactory(string databaseName = "TMS_Database") 
        {

            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options);

            UserService = new UserService(Context);

        }

        public UserService UserService { get; set; }
    }
}
