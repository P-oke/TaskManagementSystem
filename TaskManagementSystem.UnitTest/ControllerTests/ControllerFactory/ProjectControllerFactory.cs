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
    public class ProjectControllerFactory
    {
        public readonly ITaskService TaskService;
        public readonly ApplicationDbContext Context;

        public ProjectControllerFactory(string database = "TMS_Database")
        {

            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: database)
                .Options);

            var projectService = new ProjectService(Context);
            ProjectController = new ProjectController(projectService);

            ProjectController.ControllerContext.HttpContext =
              new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };

        }

        public ProjectController ProjectController { get; set; }
    }
}
