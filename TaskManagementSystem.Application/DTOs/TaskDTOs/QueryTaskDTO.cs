using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.Application.DTOs.TaskDTOs
{
    public class QueryTaskDTO : BaseSearchViewModel
    {
        public Priority? Priority { get; set; }
        public Status? Status { get; set; }
    }
}
