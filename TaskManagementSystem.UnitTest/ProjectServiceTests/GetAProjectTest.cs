using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    public class GetAProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public GetAProjectTest()
        {
            _fac = new ProjectServiceFactory();      
        }

        [Fact]
        public async Task GetAProject_ShouldReturnAProjectThatExist() 
        {
            //Arrange


            //Act

            //Create a Task
            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.AProject(project.Id);

            //Assert
            Assert.False(result.HasError);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task GetAProject_ShouldReturnErrorForAProjectThatDoesnotExist()  
        {
            //Arrange

            //Act

            var result = await _fac.ProjectService.AProject(Guid.NewGuid());

            //Assert
            Assert.True(result.HasError);

        }
    }
}
