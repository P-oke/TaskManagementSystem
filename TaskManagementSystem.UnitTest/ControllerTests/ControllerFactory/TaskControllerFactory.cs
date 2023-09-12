using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.API.Controllers;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory
{
    public class TaskControllerFactory
    {
        public readonly ITaskService TaskService;
        public Mock<IBackgroundJobClient> JobClient = new();
        public readonly ApplicationDbContext Context;

        public TaskControllerFactory(string database = "TMS_Database")
        {      

             Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: database)
                 .Options);
           
            var taskService = new TaskService(Context, JobClient.Object);
            TaskController = new TaskController(taskService);

            TaskController.ControllerContext.HttpContext =
              new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };

        }

        public TaskController TaskController { get; set; }
    }
}
