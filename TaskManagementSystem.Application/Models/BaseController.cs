using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Models
{
    public class BaseController : ControllerBase
    {
        private readonly ILog _logger;

        public BaseController()
        {
            _logger = LogManager.GetLogger(typeof(BaseController));
        }


        protected DateTime CurrentDateTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        protected Guid UserId
        {
            get { return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value); }
        }

        /// <summary>
        /// APIs the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="codes">The codes.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>IActionResult.</returns>
        public IActionResult ApiResponse<T>(T data = default, string message = "",
            ApiResponseCode codes = ApiResponseCode.OK, int? totalCount = 0, params string[] errors)
        {
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? codes.GetDescription();
            response.ResponseCode = codes.GetDescription();
            response.Code = codes;

            switch (codes)
            {
                case ApiResponseCode.CREATED:
                    {
                        return StatusCode(StatusCodes.Status201Created, response); // 201 Created
                    }
                case ApiResponseCode.INVALID_REQUEST:
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, response); // 400 Bad Request
                    }
                case ApiResponseCode.UNAUTHORIZED:
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized, response); // 401 Unauthorized
                    }
                case ApiResponseCode.NOT_FOUND:
                    {
                        return StatusCode(StatusCodes.Status404NotFound, response); // 404 Not Found
                    }
                default:
                    return Ok(response); // 200 OK (default)
            }
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {

            _logger.Error(ex.StackTrace, ex);

            var rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCode.ERROR;
#if DEBUG
            rsp.Description = $"Error: {ex?.InnerException?.Message ?? ex.Message} --> {ex?.StackTrace}";
            return StatusCode(500, rsp);
#else
            rsp.Description = customErrorMessage ?? "An error occurred while processing your request!";
            return StatusCode(500, rsp);
#endif
        }
    }
}
