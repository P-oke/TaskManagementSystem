using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.DTOs.UserDTOs;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResultModel<List<UserDTO>>> AllUsers(); 
        Task<ResultModel<PaginatedList<UserDTO>>> AllUsers(BaseSearchViewModel model);
        Task<ResultModel<PaginatedList<UserAndTaskDTO>>> AllUsersAndTasks(BaseSearchViewModel model);
        Task<ResultModel<bool>> DeleteUser(Guid userId);


    }
}
