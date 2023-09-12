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

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class TaskServiceFactory
    {
        public readonly ApplicationDbContext Context;
        public Mock<IBackgroundJobClient> JobClient = new();

        public TaskServiceFactory(string databaseName = "TMS_Database") 
        {

             Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName:  databaseName)
                 .Options);

            TaskService = new TaskService(Context, JobClient.Object);

        }

        public TaskService TaskService { get; set; }
    }
}
