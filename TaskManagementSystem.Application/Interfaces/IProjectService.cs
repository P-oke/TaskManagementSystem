using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.ProjectDTO;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ResultModel<ProjectDTO>> CreateProject(CreateProjectDTO model, Guid userId);
        Task<ResultModel<ProjectDTO>> UpdateProject(Guid projectId, UpdateProjectDTO model, Guid userId);
        Task<ResultModel<bool>> DeleteProject(Guid projectId);
        Task<ResultModel<ProjectDTO>> AProject(Guid projectId);
        Task<ResultModel<List<ProjectDTO>>> GetAllProjects();
        Task<ResultModel<PaginatedList<ProjectDTO>>> GetAllProjects(BaseSearchViewModel model);
        Task<ResultModel<List<ProjectDTO>>> GetAUserProject(Guid userId);
        Task<ResultModel<PaginatedList<ProjectDTO>>> GetAUserProject(Guid userId, BaseSearchViewModel model); 

    }
}
