using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enum;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;               
        }

        public async Task<ResultModel<NotificationDTO>> ANotification(Guid notificationId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);

            if (notification is null)
            {
                return new ResultModel<NotificationDTO>(ResponseMessage.NotificationDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            NotificationDTO notificationDTO = notification;

            return new ResultModel<NotificationDTO>(notificationDTO, ResponseMessage.SuccessMessage000);
        }

        public async Task<ResultModel<List<NotificationDTO>>> AUserNotifications(Guid userId)
        {
            var userNotifications = await _context.Notifications.Where(x => x.UserId == userId).ToListAsync();

            var data = userNotifications.OrderByDescending(x => x.CreatedOn).Select(x => (NotificationDTO)x).ToList();

            return new ResultModel<List<NotificationDTO>>(data, ResponseMessage.SuccessMessage000, ApiResponseCode.OK);

        }

        public async Task<ResultModel<PaginatedList<NotificationDTO>>> AUserNotificationsPaginated(Guid userId, BaseSearchViewModel model)
        {
            var query =  _context.Notifications.Where(x => x.UserId == userId);

            var pagedNotifications = await query.OrderByDescending(x => x.CreatedOn).PaginateAsync(model.PageIndex, model.PageSize);

            var data = pagedNotifications.Select(x => (NotificationDTO)x).ToList();

            return new ResultModel<PaginatedList<NotificationDTO>>(new PaginatedList<NotificationDTO>(data, model.PageIndex, model.PageSize, query.Count()), $"FOUND {data.Count} NOTIFICATIONS", ApiResponseCode.OK);
        }

        public async Task<ResultModel<NotificationDTO>> CreateNotification(CreateNotificationDTO model, Guid userId)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                return new ResultModel<NotificationDTO>(ResponseMessage.ErrorMessage000, ApiResponseCode.NOT_FOUND);
            }

            var notification = new Notification
            {
                Message = model.Message,
                NotificationType = model.NotificationType,
                UserId = userId
            };

            await _context.Notifications.AddAsync(notification);    
            await _context.SaveChangesAsync();

            NotificationDTO notificationDTO = notification;

            return new ResultModel<NotificationDTO>(notificationDTO, ResponseMessage.NotificationSuccessfullyCreated, ApiResponseCode.CREATED);

        }

        public async Task<ResultModel<bool>> DeleteNotification(Guid notificationId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);

            if (notification is null)
            {
                return new ResultModel<bool>(ResponseMessage.NotificationDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.NotificationSuccessfullyDeleted, ApiResponseCode.OK);
        }

        public async Task<ResultModel<bool>> MarkAsReadOrUnRead(MarkAsReadDTO model, Guid userId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == model.NotificationId);

            if (notification is null)
            {
                return new ResultModel<bool>(ResponseMessage.NotificationDoesNotExist, ApiResponseCode.NOT_FOUND);
            }

            string message = "";
            switch (model.MarkAsRead)
            {
                case true:
                    {
                        notification.IsRead = true;
                        message = "MARK AS READ";
                    }                   
                    break;
                case false:
                    {
                        notification.IsRead = false;
                        message = "MARK AS UNREAD";
                    }
                    break;
            }

            notification.ModifiedBy = userId;
            notification.ModifiedOn = DateTime.UtcNow;
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, $"SUCCESSFULLY {message}.", ApiResponseCode.OK);
        }

        public async Task<ResultModel<bool>> SendNotificationForTasksCompleted()
        {

            var completedTasks = _context.Tasks
                .Where(task => task.Status == Status.Completed)
                .ToList();

            var notification = new List<Notification>();

            completedTasks.ForEach(task => notification.Add(new Notification
            {
                Message = $"Great news! Your task {task.Title} has been successfully completed.",
                NotificationType = NotificationType.Task_Status_Update,
                UserId = task.CreatorUserId,
            }));

            await _context.AddRangeAsync(notification);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.SuccessMessage000);

        }

        public async Task<ResultModel<bool>> SendNotificationForTasksDueDateWithin48Hours()
        {
            DateTime now = DateTime.Now;
            DateTime cutoffDateTime = now.AddHours(48);

            var tasksWithin48Hours = await _context.Tasks
                .Where(task => task.DueDate >= now && task.DueDate <= cutoffDateTime)
                .ToListAsync();

            var notification = new List<Notification>();

            tasksWithin48Hours.ForEach(task => notification.Add(new Notification
            {
                Message = $"Reminder: You have a task {task.Title} due within 48 hours. Please check your tasks for details",
                NotificationType = NotificationType.Task_Due_Date,
                UserId = task.CreatorUserId,              
            }));

            await _context.AddRangeAsync(notification);
            await _context.SaveChangesAsync();

            return new ResultModel<bool>(true, ResponseMessage.SuccessMessage000);

        }
    }
}
