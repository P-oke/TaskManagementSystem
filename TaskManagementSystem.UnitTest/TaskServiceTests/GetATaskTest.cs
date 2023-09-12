using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class GetATaskTest
    {
        private readonly TaskServiceFactory _fac;

        public GetATaskTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async Task GetATask_ShouldReturnTheTaskThatExist()
        {
            //Arrange
            Guid userId = TestData.userId;


            //Act

            //Create a Task
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.ATask(task.Id);

            //Assert
            Assert.False(result.HasError);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task GetATask_ShouldReturnErrorForATaskThatDoesnotExist() 
        {
            //Arrange

            //Act

            var result = await _fac.TaskService.ATask(Guid.NewGuid());

            //Assert
            Assert.True(result.HasError);

        }
    }
}
