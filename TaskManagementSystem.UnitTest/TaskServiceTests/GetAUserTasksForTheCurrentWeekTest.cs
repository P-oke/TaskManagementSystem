using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Models;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class GetAUserTasksForTheCurrentWeekTest
    {
        private readonly TaskServiceFactory _fac;

        public GetAUserTasksForTheCurrentWeekTest()
        {
            _fac = new TaskServiceFactory();

        }

        [Fact]
        public async Task GetAUserTasksForTheWeek_ShouldReturn_ListOfUserTasksForTheWeek() 
        {
            //Arrange
            var userId = TestData.userId;

            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.GetAUserTasksForTheCurrentWeek(userId);

            //Assert
            Assert.False(result.HasError);
            Assert.IsType<List<TaskDTO>>(result.Data);

        }

        [Fact]
        public async Task GetAUserTasksForTheWeek_ShouldReturn_PaginatedUserTasksForTheWeek()
        {
            //Arrange
            var userId = TestData.userId;

            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.GetAUserTasksForTheCurrentWeekPaginated(userId, new BaseSearchViewModel());

            //Assert
            Assert.False(result.HasError);
            Assert.IsType<PaginatedList<TaskDTO>>(result.Data);

        }

    }
}
