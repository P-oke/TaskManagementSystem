using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Application.DTOs.UserDTO;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.API.Controllers
{
    /// <summary>
    /// class Authentication Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authenticationService">the authenticationService</param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }

        /// <summary>
        /// LOGIN
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ResultModel<JwtResponseDTO>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                var result = await _authenticationService.Login(model, CurrentDateTime);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// REGISTER
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ResultModel<RegisterUserDTO>), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            try
            {
                var result = await _authenticationService.Register(model);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// GENERATE REFRESH TOKEN
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Refresh-Token")]
        [ProducesResponseType(typeof(ResultModel<JwtResponseDTO>), 200)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            try
            {
                var result = await _authenticationService.RefreshToken(model.AccessToken, model.RefreshToken);

                return ApiResponse(message: result.Message, codes: result.ApiResponseCode, data: result.Data, errors: result.ErrorMessages.ToArray());

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
    }
}