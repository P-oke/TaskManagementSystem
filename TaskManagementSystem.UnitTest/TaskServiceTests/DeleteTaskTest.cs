using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class DeleteTaskTest
    {
        private readonly TaskServiceFactory _fac;

        public DeleteTaskTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async Task DeleteATask_ShoudDeleteSuccessfully() 
        {
            //Arrange
            Guid taskId = TestData.TaskId;


            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.DeleteTask(task.Id);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task DeleteATask_ShoudReturnError_WhenATaskDoesNotExist() 
        {
            //Arrange
            Guid taskId = TestData.TaskId;


            //Act

            var result = await _fac.TaskService.DeleteTask(taskId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
