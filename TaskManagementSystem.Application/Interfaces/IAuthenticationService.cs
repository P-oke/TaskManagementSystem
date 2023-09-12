using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Application.DTOs.UserDTO;
using TaskManagementSystem.Application.Utils;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResultModel<RegisterUserDTO>> Register(RegisterUserDTO model);
        Task<ResultModel<JwtResponseDTO>> Login(LoginDTO model, DateTime CurrentDateTime);
        Task<ResultModel<JwtResponseDTO>> RefreshToken(string AccessToken, string RefreshToken);
    }
}
