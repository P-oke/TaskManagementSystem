using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class CreateATaskTest
    {
        private readonly TaskServiceFactory _fac;

        public CreateATaskTest()
        {
            _fac = new TaskServiceFactory("Database");
                
        }

        [Fact]
        public async Task CreateATask_ShoudBeCreatedSuccessfully()
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act

            var result = await _fac.TaskService.CreateTask(TestData.CreateTaskDTO(), userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task CreateATask_ShoudReturnError_WhenATaskTitleExist() 
        {
            //Arrange
            Guid userId = TestData.userId;


            //Act

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.CreateTask(TestData.CreateTaskDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
