using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class GetAllTasksTest
    {
        private readonly TaskServiceFactory _fac;

        public GetAllTasksTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasks_ShouldReturn_AListOfTasks() 
        {
            //Arrange

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAllTasks(); 


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasks_ShouldReturn_PaginatedListOfTasks() 
        {
            //Arrange

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAllTasks(new BaseSearchViewModel());

            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }
    }
}
