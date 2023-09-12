using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.ProjectDTO;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context; 

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;               
        }

        public async Task<ResultModel<ProjectDTO>> AProject(Guid projectId)
        {
            var project = await _context
                                  .Projects
                                  .Where(x => x.Id == projectId)
                                  .Include(x => x.Tasks)
                                  .FirstOrDefaultAsync();

            if (project is null)
            {
                return new ResultModel<ProjectDTO>(ResponseMessage.ProjectDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            ProjectDTO projectDTO = project;
            projectDTO.TaskDTOs = project.Tasks.Select(x => (TaskDTO)x).ToList();

            return new ResultModel<ProjectDTO>(project, ResponseMessage.SuccessMessage000);
        }

        public async Task<ResultModel<ProjectDTO>> CreateProject(CreateProjectDTO model, Guid userId)
        {
            var checkProject = await _context.Projects.FirstOrDefaultAsync(x => x.Name.Replace(" ", "").ToLower() == model.Name.Replace(" ", "").ToLower());

            if (checkProject is null)
            {
                return new ResultModel<ProjectDTO>(ResponseMessage.ProjectWithNameExist, ApiResponseCode.INVALID_REQUEST);
            }

            var newProject = new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatorUserId = userId,
            };

            await _context.Projects.AddAsync(newProject);
            ProjectDTO projectDTO = newProject;

            return new ResultModel<ProjectDTO>(projectDTO, ResponseMessage.ProjectSuccessfullyCreated, ApiResponseCode.CREATED);
        }

        public async Task<ResultModel<bool>> DeleteProject(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);

            if (project is null)
            {
                return new ResultModel<bool>(ResponseMessage.ProjectDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            _context.Projects.Remove(project);

            return new ResultModel<bool>(true, ResponseMessage.ProjectSuccessfullyDeleted, ApiResponseCode.NO_CONTENT);
        }

        public async Task<ResultModel<List<ProjectDTO>>> GetAllProjects()
        {
            var query = _context.Projects
                   .Include(x => x.Tasks)
                   .OrderByDescending(x => x.CreatedOn)
                   .AsQueryable();

            var data = await query.Select(x => (ProjectDTO)x).ToListAsync();

            return new ResultModel<List<ProjectDTO>>(data, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);

        }

        public async Task<ResultModel<PaginatedList<ProjectDTO>>> GetAllProjects(BaseSearchViewModel model)
        {
            var query = _context.Projects
                    .Include(x => x.Tasks)
                    .OrderByDescending(x => x.CreatedOn)
                    .AsQueryable();

            var paginatedProjects = await query.PaginateAsync(model.PageIndex, model.PageSize); 

            var data = paginatedProjects.Select(x => (ProjectDTO)x).ToList();

            return new ResultModel<PaginatedList<ProjectDTO>>(new PaginatedList<ProjectDTO>(data, model.PageIndex, model.PageSize, query.Count()), $"FOUND {data.Count} PROJECTS", ApiResponseCode.OK);
        }

        public async Task<ResultModel<List<ProjectDTO>>> GetAUserProject(Guid userId)
        {
            var UserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userProjects = _context.Tasks.Where(x => UserTasks.Contains(x.Id)).Select(x => x.ProjectId).ToList();

            var projectDTOs = await _context.Projects.Include(x => x.Tasks).Where(x => userProjects.Contains(x.Id)).Select(x => (ProjectDTO)x).ToListAsync();

            return new ResultModel<List<ProjectDTO>>(projectDTOs, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);
        }

        public async Task<ResultModel<PaginatedList<ProjectDTO>>> GetAUserProject(Guid userId, BaseSearchViewModel model)
        {
            var UserTasks = _context.UserTasks.Where(x => x.UserId == userId).Select(x => x.TaskId).ToList();

            var userProjects = _context.Tasks.Where(x => UserTasks.Contains(x.Id)).Select(x => x.ProjectId).ToList();

            var projectDTOs = await _context.Projects.Include(x => x.Tasks).Where(x => userProjects.Contains(x.Id)).PaginateAsync(model.PageIndex, model.PageSize);

            var data = projectDTOs.Select(x => (ProjectDTO)x).ToList();

            return new ResultModel<PaginatedList<ProjectDTO>>(new PaginatedList<ProjectDTO>(data, model.PageIndex, model.PageSize, projectDTOs.Count), $"FOUND {data.Count} PROJECTS", ApiResponseCode.OK);

        }

        public async Task<ResultModel<ProjectDTO>> UpdateProject(Guid projectId, UpdateProjectDTO model, Guid userId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);

            if (project is null)
            {
                return new ResultModel<ProjectDTO>(ResponseMessage.ProjectDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            project.Name = string.IsNullOrWhiteSpace(model.Name) ? project.Name : project.Name;
            project.Description = string.IsNullOrWhiteSpace(model.Description) ? project.Description : model.Description;
            project.ModifiedOn = DateTime.UtcNow;
            project.ModifiedBy = userId;

            _context.Projects.Update(project);

            ProjectDTO projectDTO = project;

            return new ResultModel<ProjectDTO>(projectDTO, ResponseMessage.ProjectSuccessfullyUpdated, ApiResponseCode.OK);

        }
    }
}
