using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.API.Controllers
{
    /// <summary>
    /// class notification controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// </summary>
        /// <param name="notificationService">the notification service</param>
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
                
        }

        /// <summary>
        /// GET A NOTIFICATION
        /// </summary>
        /// <param name="notificationId">the notificationId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{notificationId}")]
        [ProducesResponseType(typeof(ResultModel<NotificationDTO>), 200)]
        public async Task<IActionResult> GetNotification(Guid notificationId) 
        {
            try
            {
                var result = await _notificationService.ANotification(notificationId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER NOTIFICATIONS
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}")]
        [ProducesResponseType(typeof(ResultModel<List<NotificationDTO>>), 200)]
        public async Task<IActionResult> GetAUserNotifications(Guid userId)
        {
            try
            {
                var result = await _notificationService.AUserNotifications(userId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.Count, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GET A USER NOTIFICATION - PAGINATED
        /// </summary>
        /// <param name="userId">the userId</param>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{userId}/Paginated")]
        [ProducesResponseType(typeof(ResultModel<List<NotificationDTO>>), 200)]
        public async Task<IActionResult> GetAUserNotificationsPaginated(Guid userId, [FromQuery] BaseSearchViewModel model)
        {
            try
            {
                var result = await _notificationService.AUserNotificationsPaginated(userId, model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, totalCount: result.Data.TotalCount, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// CREATE A NOTIFICATION
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResultModel<NotificationDTO>), 200)]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDTO model) 
        {
            try
            {
                var result = await _notificationService.CreateNotification(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// DELETE A NOTIFICATION
        /// </summary>
        /// <param name="notificationId">the notificationId</param>
        /// <returns></returns>
        [HttpDelete("{notificationId}")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> DeleteNotification(Guid notificationId)
        {
            try
            {
                var result = await _notificationService.DeleteNotification(notificationId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// MARK A NOTIFICATION AS READ OR UNREAD
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPut("Mark-As-Read-Or-UnRead")]
        [ProducesResponseType(typeof(ResultModel<bool>), 200)]
        public async Task<IActionResult> MarkAsReadOrUnRead([FromBody] MarkAsReadDTO model)
        {
            try
            {
                var result = await _notificationService.MarkAsReadOrUnRead(model, UserId);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
