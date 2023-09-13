using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class UpdateTaskTest
    {
        private readonly TaskServiceFactory _fac;

        public UpdateTaskTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async Task UpdateATask_ShoudBeUpdatedSuccessfully()
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.UpdateTask(task.Id, TestData.UpdateTaskDTO(), userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task UpdateATask_ShoudReturnError_WhenATaskDoesNotExist() 
        {
            //Arrange
            Guid userId = TestData.userId;
            Guid taskId = TestData.TaskId;


            //Act

            var result = await _fac.TaskService.UpdateTask(taskId, TestData.UpdateTaskDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
