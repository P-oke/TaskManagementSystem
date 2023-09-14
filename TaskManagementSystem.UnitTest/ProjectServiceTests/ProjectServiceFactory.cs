using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    public class ProjectServiceFactory
    {
        public readonly ApplicationDbContext Context; 

        public ProjectServiceFactory(string databaseName = "TMS_Database")
        {

            Context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options);

            ProjectService = new ProjectService(Context);

        }

        public ProjectService ProjectService { get; set; }
    }
}
