using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface INotificationService
    {
        Task<ResultModel<NotificationDTO>> CreateNotification(CreateNotificationDTO model, Guid userId);
        Task<ResultModel<bool>> MarkAsReadOrUnRead(MarkAsReadDTO model, Guid userId); 
        Task<ResultModel<bool>> DeleteNotification(Guid notificationId); 
        Task<ResultModel<NotificationDTO>> ANotification(Guid notificationId);
        Task<ResultModel<List<NotificationDTO>>> AUserNotifications(Guid userId);
        Task<ResultModel<PaginatedList<NotificationDTO>>> AUserNotificationsPaginated(Guid userId, BaseSearchViewModel model);
        Task<ResultModel<bool>> SendNotificationForTasksDueDateWithin48Hours();
        Task<ResultModel<bool>> SendNotificationForTasksCompleted();
    }
}
