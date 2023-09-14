using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    public class DeleteProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public DeleteProjectTest()
        {
            _fac = new ProjectServiceFactory();    
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAProject_ShoudDeleteSuccessfully()
        {
            //Arrange

            //Act
            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.DeleteProject(project.Id);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteAProject_ShoudReturnError_WhenAProjectDoesNotExist()  
        {
            //Arrange
            Guid projectId = TestData.ProjectId;


            //Act

            var result = await _fac.ProjectService.DeleteProject(projectId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
