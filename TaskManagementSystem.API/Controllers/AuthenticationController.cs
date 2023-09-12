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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }

        [HttpPost]
        [Route("login")]
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


        [HttpPost]
        [Route("register")]
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

        [HttpPost]
        [Route("refresh-token")]
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