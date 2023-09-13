using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Enum;
 
namespace TaskManagementSystem.Application.DTOs.TaskDTOs
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }

        public static implicit operator TaskDTO(TaskManagementSystem.Domain.Entities.Task model)
        {
            return model == null ? null : new TaskDTO
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Priority = model.Priority.GetDescription(),
                Status = model.Status.GetDescription(),
                CreatedOn = model.CreatedOn
            };

        }

    }

    public class AssignAndRemoveTaskFromProjectDTO    
    {
        public Guid ProjectId { get; set; }
        public Guid TaskId { get; set; } 
    }

}
