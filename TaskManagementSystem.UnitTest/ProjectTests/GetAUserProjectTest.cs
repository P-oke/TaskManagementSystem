using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    public class GetAUserProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public GetAUserProjectTest()
        {
            _fac = new ProjectServiceFactory();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserProjects_ShouldReturn_AListUserProjects()
        {
            //Arrange
            Guid userId = TestData.userId;

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();


            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            task.ProjectId = project.Id;
            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.ProjectService.GetAUserProject(userId);


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }


        [Fact]
        public async System.Threading.Tasks.Task GetAUserProjects_ShouldReturn_PaginatedUserProjects()
        {
            //Arrange
            Guid userId = TestData.userId;

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();


            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            task.ProjectId = project.Id;
            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.ProjectService.GetAUserProject(userId, new BaseSearchViewModel());


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }
    }
}
