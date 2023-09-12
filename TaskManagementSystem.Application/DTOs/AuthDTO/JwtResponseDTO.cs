using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.DTOs.AuthDTO
{
    public class JwtResponseDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> Permissions { get; set; }
    }
}
