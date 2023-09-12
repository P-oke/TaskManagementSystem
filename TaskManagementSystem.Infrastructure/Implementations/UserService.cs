using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.DTOs.UserDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultModel<List<UserDTO>>> AllUsers()
        {
            var users = await _context.Users.OrderByDescending(x => x.CreationTime).ToListAsync();

            var usersDTO = users.Select(x => (UserDTO)x).ToList();

            return new ResultModel<List<UserDTO>>(usersDTO, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);

        }

        public async Task<ResultModel<PaginatedList<UserDTO>>> AllUsers(BaseSearchViewModel model)
        {
            var query = _context.Users.OrderByDescending(x => x.CreationTime).AsQueryable();

            query = BuildQueryFilter(query, model);

            var paginatedUsers = await query.PaginateAsync(model.PageIndex, model.PageSize);

            var data = paginatedUsers.Select(x => (UserDTO)x).ToList();

            return new ResultModel<PaginatedList<UserDTO>>(new PaginatedList<UserDTO>(data, model.PageIndex, model.PageSize, query.Count()), $"FOUND {data.Count} USERS", ApiResponseCode.OK);
        }

        private static IQueryable<User> BuildQueryFilter(IQueryable<User> query, BaseSearchViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                var keyword = model.Keyword.ToLower();

                query = query.Where(x => x.FirstName.ToLower().Contains(keyword) || x.FirstName.ToLower().Contains(keyword) || x.Email.ToLower().Contains(keyword));
            }

            return query;

        }

        public async Task<ResultModel<PaginatedList<UserAndTaskDTO>>> AllUsersAndTasks(BaseSearchViewModel model)
        {
            var query = _context.Users.OrderByDescending(x => x.CreationTime).AsQueryable();

            query = BuildQueryFilter(query, model);

            var paginatedUsers = await query.PaginateAsync(model.PageIndex, model.PageSize);

            var usersAndTaskDTOs = paginatedUsers.Select(x => (UserAndTaskDTO)x).ToList();

            var tasks = _context.Tasks;

            usersAndTaskDTOs.ForEach(x =>
            {
                x.TaskDTOs = tasks.Where(y => y.CreatorUserId == x.Id).Select(x => (TaskDTO)x).ToList();
            });

            return new ResultModel<PaginatedList<UserAndTaskDTO>>(new PaginatedList<UserAndTaskDTO>(usersAndTaskDTOs, model.PageIndex, model.PageSize, query.Count()), $"FOUND {usersAndTaskDTOs.Count} USERS WITH THEIR TASKS", ApiResponseCode.OK);

        } 

        public async Task<ResultModel<bool>> DeleteUser(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return new ResultModel<bool>(ResponseMessage.ErrorMessage000, ApiResponseCode.NOT_FOUND);
            }

            var userNotifications = await _context.Notifications.Where(x => x.UserId == userId).ToListAsync();
            _context.RemoveRange(userNotifications);

            _context.Remove(user);

            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.SuccessfullyDeletedAUser, ApiResponseCode.NO_CONTENT);

        }
    }
}
