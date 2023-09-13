using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.API.Controllers
{
    /// <summary>
    /// class Task Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="taskService">the taskService</param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;

        }

        /// <summary>
        /// GET A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{taskId}")]
        [ProducesResponseType(typeof(ResultModel<TaskDTO>), 200)]
        public async Task<IActionResult> GetTask([FromBody] Guid taskId)
        {
            try
            {
                var result = await _taskService.ATask(taskId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// CREATE A TASK
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultModel<TaskDTO>), 200)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO model)
        {
            try
            {
                var result = await _taskService.CreateTask(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// DELETE A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{taskId}")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> DeleteTask(Guid taskId) 
        {
            try
            {
                var result = await _taskService.DeleteTask(taskId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}")]
        [ProducesResponseType(typeof(ResultModel<List<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTask(Guid userId)
        {
            try
            {
                var result = await _taskService.GetAUserTasks(userId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}/Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTask(Guid userId, [FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _taskService.GetAUserTasks(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS BY STATUS OR PRIORITY
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User-Tasks-By-Status-Or-Priority/{userId}")]
        [ProducesResponseType(typeof(ResultModel<List<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTask(Guid userId, [FromQuery] QueryTaskDTO model)
        {
            try
            {
                var result = await _taskService.GetAUserTasks(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS BY STATUS OR PRIORITY - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User-Tasks-By-Status-Or-Priority/{userId}/Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTaskPaginated(Guid userId, [FromQuery] QueryTaskDTO model)
        {
            try
            {
                var result = await _taskService.GetAUserTasksPaginated(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// UPDATE A TASK
        /// </summary>
        /// <param name="taskId">the taskId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{taskId}")]
        [ProducesResponseType(typeof(ResultModel<TaskDTO>), 200)]
        public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] UpdateTaskDTO model)
        {
            try
            {
                var result = await _taskService.UpdateTask(taskId, model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// ASSIGN A TASK TO A PROJECT
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Assign-Task-To-Project")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> AssignTaskToProject([FromBody] AssignAndRemoveTaskFromProjectDTO model)
        {
            try
            {
                var result = await _taskService.AssignTaskToAProject(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// REMOVE A TASK FROM A PROJECT
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Remove-Task-From-Project")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> RemoveTaskFromProject([FromBody] AssignAndRemoveTaskFromProjectDTO model)
        {
            try
            {
                var result = await _taskService.RemoveTaskFromProject(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS FOR CURRENT WEEK
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User-Tasks-For-Current-Week/{userId}")]
        [ProducesResponseType(typeof(ResultModel<List<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTasksForTheCurrentWeek(Guid userId)
        {
            try
            {
                var result = await _taskService.GetAUserTasksForTheCurrentWeek(userId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER TASKS FOR CURRENT WEEK - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User-Tasks-For-Current-Week/{userId}/Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAUserTasksForTheCurrentWeek(Guid userId, [FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _taskService.GetAUserTasksForTheCurrentWeekPaginated(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL TASKS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<List<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var result = await _taskService.GetAllTasks();

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL TASKS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<TaskDTO>>), 200)]
        public async Task<IActionResult> GetAllTasks([FromQuery] BaseSearchViewModel model) 
        {
            try
            {
                var result = await _taskService.GetAllTasks(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


    }
}
