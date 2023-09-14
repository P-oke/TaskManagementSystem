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

namespace TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory
{
    public class ProjectControllerExceptionTestFactory
    {
        public readonly Mock<IProjectService> ProjectService = new();
        public readonly Mock<ApplicationDbContext> Context = new(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

        public ProjectControllerExceptionTestFactory()
        {

            ProjectController = new ProjectController(ProjectService.Object); 

            ProjectController.ControllerContext.HttpContext =
              new DefaultHttpContext { User = TestData.GetAuthenticatedUser() };

        }

        public ProjectController ProjectController { get; set; }
    }
}
