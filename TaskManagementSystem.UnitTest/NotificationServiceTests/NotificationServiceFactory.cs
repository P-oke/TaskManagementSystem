using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class NotificationServiceFactory
    {
        public readonly ApplicationDbContext Context;

        public NotificationServiceFactory(string databaseName = "TMS_Database")
        {

            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: databaseName)
               .Options);

            NotificationService = new NotificationService(Context);
        }

        public NotificationService NotificationService { get; set; } 

    }
}
