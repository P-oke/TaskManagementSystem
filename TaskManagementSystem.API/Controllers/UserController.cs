using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.DTOs.UserDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Infrastructure.Implementations;
using static TaskManagementSystem.Application.Models.Constant;

namespace TaskManagementSystem.API.Controllers
{
    /// <summary>
    /// class User Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">the userService</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// ALL USERS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ResultModel<List<UserDTO>>), 200)]
        public async Task<IActionResult> AllUsers() 
        {
            try
            {
                var result = await _userService.AllUsers();

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// ALL USERS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<UserDTO>>), 200)]
        public async Task<IActionResult> AllUsers([FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _userService.AllUsers(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// ALL USERS AND TASKS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Users-And-Task-Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<UserAndTaskDTO>>), 200)]
        public async Task<IActionResult> AllUsersAndTasks([FromQuery] BaseSearchViewModel model) 
        {
            try
            {
                var result = await _userService.AllUsersAndTasks(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        /// <summary>
        /// DELETE USER
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = AppRole.SUPERADMIN)]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> DeleteUser(Guid userId) 
        {
            try
            {
                var result = await _userService.DeleteUser(userId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
