using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.DTOs.ProjectDTO;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.API.Controllers
{
    /// <summary>
    /// class project controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="projectService">the project service</param>
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;  
        }

        /// <summary>
        /// GET A PROJECT
        /// </summary>
        /// <param name="projectId">the projectId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(ResultModel<ProjectDTO>), 200)]
        public async Task<IActionResult> GetProject(Guid projectId) 
        {
            try
            {
                var result = await _projectService.AProject(projectId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// CREATE PROJECT
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultModel<ProjectDTO>), 200)]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO model)
        {
            try
            {
                var result = await _projectService.CreateProject(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// DELETE A PROJECT
        /// </summary>
        /// <param name="projectId">the projectId</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> DeleteProject(Guid projectId)
        {
            try
            {
                var result = await _projectService.DeleteProject(projectId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        } 


        /// <summary>
        /// UPDATE A PROJECT
        /// </summary>
        /// <param name="projectId">the projectId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(ResultModel<ProjectDTO>), 200)]
        public async Task<IActionResult> UpdateProject(Guid projectId, [FromBody] UpdateProjectDTO model)
        {
            try
            {
                var result = await _projectService.UpdateProject(projectId, model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL PROJECTS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ResultModel<ProjectDTO>), 200)]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var result = await _projectService.GetAllProjects();

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET ALL PROJECTS - PAGINATED
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Paginated")]
        [ProducesResponseType(typeof(ResultModel<ProjectDTO>), 200)]
        public async Task<IActionResult> GetAllProjects([FromQuery] BaseSearchViewModel model )
        {
            try
            {
                var result = await _projectService.GetAllProjects(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER PROJECTS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}")]
        [ProducesResponseType(typeof(ResultModel<List<ProjectDTO>>), 200)]
        public async Task<IActionResult> GetAUserProjects(Guid userId)
        {
            try
            {
                var result = await _projectService.GetAUserProject(userId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// UPDATE A PROJECT
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}/Paginated")]
        [ProducesResponseType(typeof(ResultModel<PaginatedList<ProjectDTO>>), 200)]
        public async Task<IActionResult> GetAUserProjectsPaginated(Guid userId, [FromQuery] BaseSearchViewModel model) 
        {
            try
            {
                var result = await _projectService.GetAUserProject(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


    }
}
