using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.UnitTests.TaskServiceTests;

namespace TaskManagementSystem.UnitTests.ProjectTests
{

    public class GetAllProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public GetAllProjectTest()
        {
            _fac = new ProjectServiceFactory();
        }


        [Fact]
        public async System.Threading.Tasks.Task GetAllProject_ShouldReturn_AListOfProjects()
        {
            
            //Arrange



            //Act

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.GetAllProjects();


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllProject_ShouldReturn_APaginatedListOfProjects()
        {

            //Arrange



            //Act

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.GetAllProjects(new BaseSearchViewModel { Keyword = "keyword"});


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }
    }
}
