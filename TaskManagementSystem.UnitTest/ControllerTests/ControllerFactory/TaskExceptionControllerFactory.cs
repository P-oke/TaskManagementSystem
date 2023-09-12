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
    public class TaskExceptionControllerFactory
    {
        public readonly Mock<ITaskService> TaskService = new();
        public readonly Mock<ApplicationDbContext> Context = new(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

        public TaskExceptionControllerFactory() 
        {
            
            TaskController = new TaskController(TaskService.Object);

            TaskController.ControllerContext.HttpContext =
              new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };

        }

        public TaskController TaskController { get; set; }
    }
}
