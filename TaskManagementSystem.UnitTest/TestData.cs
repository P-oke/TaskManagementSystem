using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.UnitTests
{
    public static class TestData
    {
        public static readonly Guid userId = Guid.Parse("32d90561-3d2d-4ca5-890a-2548b80176a4");
        public static readonly Guid TaskId = Guid.Parse("4cb78d8e-f99b-4b31-90a3-5803f2d04a92");
        public static readonly string Password = "Dev@1234";
        public static readonly string AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjMyZDkwNTYxLTNkMmQtNGNhNS04OTBhLTI1NDhiODAxNzZhNCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiIgIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoicGF1bHRtc0B5b3BtYWlsLmNvbSIsImp0aSI6ImMyYTlmODM5LWE4ZmYtNDg3OC1hODE1LWJkNmEyZGM2M2IxYSIsIm5iZiI6MTY5NDUyODM5OSwiZXhwIjoxNjk0NTI4Njk5LCJpc3MiOiJWYWxpZElzc3VlciIsImF1ZCI6IlZhbGlkQXVkaWVuY2UifQ.R3s7H9iAJEmx48-f2zUjyyAqWwXVWLuif2Kq_Swgen8";
        
        public static PasswordHasher<User> Hasher { get; set; } = new PasswordHasher<User>();

        public static CreateTaskDTO CreateTaskDTO()
        {
            return new CreateTaskDTO
            {
                Title = "Test",
                Description = "Description",
                Priority = Priority.Low,
                Status = Status.Pending,

            };
        }

        public static UpdateTaskDTO UpdateTaskDTO()
        {
            return new UpdateTaskDTO
            {
                Title = "Test Updated",
                Description = "Description",
                Priority = Priority.Low,
                Status = Status.Pending,

            };
        }

        public static Domain.Entities.Task Task() 
        {
            return new Domain.Entities.Task
            {
                Title = "Test",
                Description = "Description",
                Priority = Priority.Low,
                Status = Status.Pending,
            };
        }


        public static Project Project()
        {
            return new Project
            {
                Name = "project",
                Description = "Project description"
            };
        }

        public static User User()
        {
            return new User
            {
                FirstName = "Paul",
                LastName = "TMS",
                Email = "paultms@yopmail.com",
                PasswordHash = Hasher.HashPassword(null, "Dev@1234"),
                PhoneNumber = "09012345678"
            };
        }

        public static Notification Notification()
        {
            return new Notification
            {
                Message = "message",
                NotificationType = NotificationType.Task_Due_Date,
                UserId = userId             
            };
        }

        public static ClaimsPrincipal GetAuthenticatedUser()
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, TestData.userId.ToString()),
            }));
        }
    }
}
