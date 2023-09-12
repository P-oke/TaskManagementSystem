using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.DTOs.ProjectDTO
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<TaskDTO> TaskDTOs { get; set; }
        

        public static implicit operator ProjectDTO(Project model)
        {
            return model == null ? null : new ProjectDTO
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = model.CreatedOn,
                TaskDTOs = model.Tasks.Select(x=> (TaskDTO)x).ToList(),
            };
        }
    }
}
