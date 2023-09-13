using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.DTOs.UserDTO;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITaskService
    {
        Task<ResultModel<TaskDTO>> CreateTask(CreateTaskDTO model, Guid userId);  
        Task<ResultModel<TaskDTO>> UpdateTask(Guid taskId, UpdateTaskDTO model, Guid userId);  
        Task<ResultModel<bool>> DeleteTask(Guid taskId); 
        Task<ResultModel<TaskDTO>> ATask(Guid taskId);  
        Task<ResultModel<List<TaskDTO>>> GetAUserTasks(Guid userId);
        Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasks(Guid userId, BaseSearchViewModel model);
        Task<ResultModel<List<TaskDTO>>> GetAUserTasks(Guid userId, QueryTaskDTO model);  
        Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasksPaginated(Guid userId, QueryTaskDTO model);
        Task<ResultModel<bool>> AssignTaskToAProject(AssignAndRemoveTaskFromProjectDTO model, Guid userId); 
        Task<ResultModel<bool>> RemoveTaskFromProject(AssignAndRemoveTaskFromProjectDTO model, Guid userId);
        Task<ResultModel<List<TaskDTO>>> GetAUserTasksForTheCurrentWeek(Guid userId);
        Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasksForTheCurrentWeekPaginated(Guid userId, BaseSearchViewModel model);
        Task<ResultModel<bool>> AssignTaskToAUser(Guid taskId, Guid userId);
        Task<ResultModel<List<TaskDTO>>> GetAllTasks();
        Task<ResultModel<PaginatedList<TaskDTO>>> GetAllTasks(BaseSearchViewModel model);

    }
}
