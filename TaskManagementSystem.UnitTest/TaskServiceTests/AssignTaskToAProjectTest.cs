﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class AssignTaskToAProjectTest
    {
        private readonly TaskServiceFactory _fac;

        public AssignTaskToAProjectTest()
        {
            _fac = new TaskServiceFactory();

        }

        [Fact]
        public async Task AssignTaskToProject_ShouldWork()
        {
            //Arrange
            var userId = TestData.userId;

            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.AssignTaskToAProject(new AssignAndRemoveTaskFromProjectDTO { ProjectId = project.Id, TaskId = task.Id }, userId);

            //Assert
            Assert.False(result.HasError);

        }

    }
}
