using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enum;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _jobClient;


        public TaskService(ApplicationDbContext context, IBackgroundJobClient jobClient)
        {
            _context = context;
            _jobClient = jobClient;
        }

        /// <summary>
        /// GET A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <returns>Task&lt;ResultModel&lt;TaskDTO&gt;&gt;</returns>
        public async Task<ResultModel<TaskDTO>> ATask(Guid taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task is null)
            {
                return new ResultModel<TaskDTO>(ResponseMessage.TaskDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            TaskDTO taskDTO = task;

            return new ResultModel<TaskDTO>(task, ResponseMessage.SuccessMessage000);

        }

        /// <summary>
        /// CREATE TASK
        /// </summary>
        /// <param name="model">the model</param>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;TaskDTO&gt;&gt;</returns>
        public async Task<ResultModel<TaskDTO>> CreateTask(CreateTaskDTO model, Guid userId)
        {
            var checkTask = await _context.Tasks.FirstOrDefaultAsync(x => x.Title.Replace(" ", "").ToLower() == model.Title.Replace(" ", "").ToLower());

            if (checkTask is not null)
            {
                return new ResultModel<TaskDTO>(ResponseMessage.TaskExistWithTitle, ApiResponseCode.INVALID_REQUEST);
            }

            var newTask = new TaskManagementSystem.Domain.Entities.Task
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Priority = model.Priority,
                Status = model.Status,
                CreatorUserId = userId,
            };

            await _context.Tasks.AddAsync(newTask);

            var userTask = new UserTask
            {
                TaskId = newTask.Id,
                UserId = userId,
            };

            await _context.UserTasks.AddAsync(userTask);
            await _context.SaveChangesAsync();

            TaskDTO taskDTO = newTask;
            return new ResultModel<TaskDTO>(taskDTO, ResponseMessage.TaskSuccessfullyCreated, ApiResponseCode.CREATED);
        }

        /// <summary>
        /// DELETE A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <returns>Task&lt;ResultModel&lt;bool&gt;&gt;</returns>
        public async Task<ResultModel<bool>> DeleteTask(Guid taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task is null)
            {
                return new ResultModel<bool>(ResponseMessage.TaskDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.TaskSuccessfullyDeleted, ApiResponseCode.OK);
        }

        /// <summary>
        /// GET A USER TASKS
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;List&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<List<TaskDTO>>> GetAUserTasks(Guid userId)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasks = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            var tasks = await userTasks.OrderByDescending(x => x.CreatedOn).Select(x => (TaskDTO)x).ToListAsync();

            return new ResultModel<List<TaskDTO>>(tasks, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);
        }

        /// <summary>
        /// GET A USER TASK - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns>Task&lt;ResultModel&lt;PaginatedList&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasks(Guid userId, BaseSearchViewModel model)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasks = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            var query = BuildQueryFilter(userTasks, model);

            var paginatedTasks = await query.OrderByDescending(x => x.CreatedOn).PaginateAsync(model.PageIndex, model.PageSize);

            var data = paginatedTasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<PaginatedList<TaskDTO>>(new PaginatedList<TaskDTO>(data, model.PageIndex, model.PageSize, userTasks.Count()), $"FOUND {data.Count} TASKS", ApiResponseCode.OK);

        }

        private static IQueryable<Domain.Entities.Task> BuildQueryFilter(IQueryable<Domain.Entities.Task> query, BaseSearchViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                var keyword = model.Keyword.ToLower();

                query = query.Where(x => x.Title.ToLower().Contains(keyword));
            }

            return query;

        }

        /// <summary>
        /// GET A USER TASKS BY FILTERING BY PRIORITY OR STATUS
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;List&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<List<TaskDTO>>> GetAUserTasks(Guid userId, QueryTaskDTO model)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasks = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            var query = BuildQueryFilter(userTasks, model);

            var tasks = await query.OrderByDescending(x => x.CreatedOn).Select(x => (TaskDTO)x).ToListAsync();

            return new ResultModel<List<TaskDTO>>(tasks, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);
        }

        private static IQueryable<Domain.Entities.Task> BuildQueryFilter(IQueryable<Domain.Entities.Task> query, QueryTaskDTO model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                var keyword = model.Keyword.ToLower();

                query = query.Where(x => x.Title.ToLower().Contains(keyword));
            }

            if (model.Priority != null)
            {
                query = query.Where(x => x.Priority == model.Priority);
            }

            if (model.Status != null)
            {
                query = query.Where(x => x.Status == model.Status);
            }

            return query;

        }

        /// <summary>
        /// GET A USER TASKS BY FILTERING BY  PRIORITY OR STATUS - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns>Task&lt;ResultModel&lt;PaginatedList&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasksPaginated(Guid userId, QueryTaskDTO model)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasks = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            var query = BuildQueryFilter(userTasks, model);

            var paginatedTasks = await query.OrderByDescending(x => x.CreatedOn).PaginateAsync(model.PageIndex, model.PageSize);

            var data = paginatedTasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<PaginatedList<TaskDTO>>(new PaginatedList<TaskDTO>(data, model.PageIndex, model.PageSize, userTasks.Count()), $"FOUND {data.Count} TASKS", ApiResponseCode.OK);
        }

        /// <summary>
        /// UPDATE A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <param name="model">the model</param>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;TaskDTO&gt;&gt;</returns>
        public async Task<ResultModel<TaskDTO>> UpdateTask(Guid taskId, UpdateTaskDTO model, Guid userId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task is null)
            {
                return new ResultModel<TaskDTO>(ResponseMessage.TaskDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            var userTasks = _context.UserTasks.Any(x => x.TaskId == taskId && x.UserId == userId);

            if (!userTasks)
            {
                return new ResultModel<TaskDTO>(ResponseMessage.TaskDoesNotExistForThisUser, ApiResponseCode.NOT_FOUND);
            }

            task.Title = string.IsNullOrWhiteSpace(model.Title) ? task.Title : model.Title;
            task.Description = string.IsNullOrWhiteSpace(model.Description) ? task.Description : model.Description;
            task.DueDate = model.DueDate;
            task.Priority = model.Priority;
            task.Status = model.Status;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            TaskDTO taskDTO = task;
            return new ResultModel<TaskDTO>(taskDTO, ResponseMessage.TaskSuccessfullyCreated, ApiResponseCode.OK);

        }

        /// <summary>
        /// ASSIGN A TASK TO A PROJECT
        /// </summary>
        /// <param name="model">the model</param>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;bool&gt;&gt;</returns>
        public async Task<ResultModel<bool>> AssignTaskToAProject(AssignAndRemoveTaskFromProjectDTO model, Guid userId)
        { 
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if (task is null)
            {
                return new ResultModel<bool>(ResponseMessage.TaskDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == model.ProjectId);

            if (project is null)
            {
                return new ResultModel<bool>(ResponseMessage.ProjectDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            if (project.Tasks.Any(x => x.Id == model.TaskId))
            {
                return new ResultModel<bool>(ResponseMessage.TaskAlreadyAssignedToAProject, ApiResponseCode.INVALID_REQUEST);
            }

            task.ProjectId = model.ProjectId;
            task.ModifiedBy = userId;
            task.ModifiedOn = DateTime.Now;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.TaskSuccessfullyAssignedToAProject, ApiResponseCode.OK);

        }

        /// <summary>
        /// REMOVE TASK FROM PROJECT
        /// </summary>
        /// <param name="model">the model</param>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;TaskDTO&gt;&gt;</returns>
        public async Task<ResultModel<bool>> RemoveTaskFromProject(AssignAndRemoveTaskFromProjectDTO model, Guid userId) 
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == model.TaskId);

            if (task is null)
            {
                return new ResultModel<bool>(ResponseMessage.TaskExistWithTitle, ApiResponseCode.NOT_FOUND);
            }

            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == model.ProjectId);

            if (project is null)
            {
                return new ResultModel<bool>(ResponseMessage.ProjectDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            var taskAndProject = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == model.TaskId && x.ProjectId == model.ProjectId);

            if (taskAndProject is null)
            {
                return new ResultModel<bool>(ResponseMessage.TaskDoesNotHaveProject, ApiResponseCode.INVALID_REQUEST);
            }

            task.ProjectId = null;
            task.ModifiedOn = DateTime.Now;
            task.ModifiedBy = userId;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.TaskSuccessfullyRemovedFromAProject, ApiResponseCode.OK);
        }

        /// <summary>
        /// GET A USER TASKS FOR CURRENT WEEK 
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<List<TaskDTO>>> GetAUserTasksForTheCurrentWeek(Guid userId)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasksQuery = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            DateTime today = DateTime.Now;
            DateTime startOfWeek = today.AddDays(-((int)today.DayOfWeek - (int)DayOfWeek.Monday));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var userTasks = await userTasksQuery.OrderByDescending(x=> x.CreatedOn).Where(task => task.DueDate >= startOfWeek && task.DueDate <= endOfWeek).ToListAsync();

            var tasks = userTasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<List<TaskDTO>>(tasks, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);
        }

        /// <summary>
        /// GET A USER TASKS FOR THE CURRENT WEEK - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns>Task&lt;ResultModel&lt;&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<PaginatedList<TaskDTO>>> GetAUserTasksForTheCurrentWeekPaginated(Guid userId, BaseSearchViewModel model)
        {
            var getUserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userTasksQuery = _context.Tasks.Where(x => getUserTasks.Contains(x.Id));

            DateTime today = DateTime.Now;
            DateTime startOfWeek = today.AddDays(-((int)today.DayOfWeek - (int)DayOfWeek.Monday));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            userTasksQuery = userTasksQuery.OrderByDescending(x => x.CreatedOn).Where(task => task.DueDate >= startOfWeek && task.DueDate <= endOfWeek);

            var paginatedTasks = await userTasksQuery.PaginateAsync(model.PageIndex, model.PageSize);

            var data = paginatedTasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<PaginatedList<TaskDTO>>(new PaginatedList<TaskDTO>(data, model.PageIndex, model.PageSize, userTasksQuery.Count()), $"FOUND {data.Count} TASKS", ApiResponseCode.OK);
        }

        /// <summary>
        /// REMOVE TASK FROM PROJECT
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <param name="userId">the userId</param>
        /// <returns>Task&lt;ResultModel&lt;TaskDTO&gt;&gt;</returns>
        public async Task<ResultModel<bool>> AssignTaskToAUser(Guid taskId, Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return new ResultModel<bool>(ResponseMessage.ErrorMessage000, ApiResponseCode.NOT_FOUND);
            }

            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task is null)
            {
                return new ResultModel<bool>(ResponseMessage.TaskDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            var userTask = new UserTask
            {
                UserId = userId,
                TaskId = taskId,
            };

            var notification = new CreateNotificationDTO
            {
                NotificationType = NotificationType.New_Task,
                Message = $"You've been assigned a new task {task.Title}. Check your tasks for details",
            };

            await _context.AddAsync(userTask);
            await _context.SaveChangesAsync();

            _jobClient.Enqueue<INotificationService>(x => x.CreateNotification(notification, userId));

            return new ResultModel<bool>(true, ResponseMessage.TaskSuccessfullyDeleted, ApiResponseCode.OK);

        }

        /// <summary>
        /// GET ALL TASK
        /// </summary>
        /// <returns>Task&lt;ResultModel&lt;List&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<List<TaskDTO>>> GetAllTasks()
        {
            var query = _context.Tasks.OrderByDescending(x => x.CreatedOn).AsQueryable();

            var tasks = await query.Select(x => (TaskDTO)x).ToListAsync();

            return new ResultModel<List<TaskDTO>>(tasks, $"SUCCESSFULLY FOUND {tasks.Count} TASKS", ApiResponseCode.OK);
        }

        /// <summary>
        /// GET ALL TASK
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns>Task&lt;ResultModel&lt;PaginatedList&lt;TaskDTO&gt;&gt;&gt;</returns>
        public async Task<ResultModel<PaginatedList<TaskDTO>>> GetAllTasks(BaseSearchViewModel model)
        {
            var query = _context.Tasks.OrderByDescending(x => x.CreatedOn).AsQueryable();

            query = BuildQueryFilter(query, model);

            var paginatedTasks = await query.PaginateAsync(model.PageIndex, model.PageSize);

            var data = paginatedTasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<PaginatedList<TaskDTO>>(new PaginatedList<TaskDTO>(data, model.PageIndex, model.PageSize, query.Count()), $"SUCCESSFULLY FOUND {data.Count} TASKS", ApiResponseCode.OK);
        }
    }
}
