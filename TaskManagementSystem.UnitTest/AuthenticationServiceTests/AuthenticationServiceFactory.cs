using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.UnitTests.AuthenticationServiceTests
{
    public class AuthenticationServiceFactory
    {
        public ApplicationDbContext Context;
        public Mock<UserManager<User>> UserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
             null, null, null, null, null, null, null, null);

        public Mock<IConfiguration> Configuration = new ();
        public Mock<ILogger<AuthenticationService>> Logger = new();

        public AuthenticationServiceFactory( string dataBaseName = "TMS_Database")
        {
            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: dataBaseName)
               .Options);

            AuthenticationService = new AuthenticationService(UserManager.Object, Context, Configuration.Object, Logger.Object);


        }

        public AuthenticationService AuthenticationService { get; set; }

    }
}
