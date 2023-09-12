using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.DTOs.UserDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }

        public static implicit operator UserDTO(User model)
        {
            return model == null ? null : new UserDTO
            {
                Id = model.Id,
                FullName = model.FullName,
                EmailAddress = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedOn = model.CreationTime,
            };
        }



    }

    public class UserAndTaskDTO : UserDTO
    {

        public List<TaskDTO> TaskDTOs { get; set; } 

        public static implicit operator UserAndTaskDTO(User model)
        {
            return model == null ? null : new UserAndTaskDTO
            {
                Id = model.Id,
                FullName = model.FullName,
                EmailAddress = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedOn = model.CreationTime,
            };
        }

    }











}
