using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Application.DTOs.UserDTO;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Models.Enums;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enum;
using TaskManagementSystem.Infrastructure.Context;

namespace TaskManagementSystem.Infrastructure.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
      
        private readonly UserManager<User> _userManager;

        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;

        private readonly ILogger<AuthenticationService> _logger;


        public AuthenticationService
        (
            UserManager<User> userManager,
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<AuthenticationService> logger
        )
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _logger = logger;            
        }

        public async Task<ResultModel<JwtResponseDTO>> Login(LoginDTO model, DateTime CurrentDateTime)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                return new ResultModel<JwtResponseDTO>(ResponseMessage.ErrorMessage507, ApiResponseCode.NOT_FOUND);
            };

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);

                var (token, expiration) = CreateJwtTokenAsync(user, userRoles, claims);

                var refreshToken = BuildRefreshToken();

                var userRefreshToken = new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    IssuedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration.GetSection("JWT:RefreshTokenExpiration").Value))
                };

                await SaveRefreshToken(new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    IssuedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration.GetSection("JWT:RefreshTokenExpiration").Value))
                });

                var data = new JwtResponseDTO()
                {
                    Token = token,
                    Expiration = expiration,
                    Roles = userRoles,
                    RefreshToken = refreshToken,
                    Permissions = claims.Select(x => x.Value).ToList(),
                };

                return new ResultModel<JwtResponseDTO>(data);
            }

            return new ResultModel<JwtResponseDTO>(ResponseMessage.ErrorMessage507, ApiResponseCode.UNAUTHORIZED);
        }

        private (string, DateTime) CreateJwtTokenAsync(User user, IList<string> userRoles, IList<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            var userClaims = BuildUserClaims(user, userRoles, claims);

            var signKey = new SymmetricSecurityKey(key);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT:ValidIssuer").Value,
                notBefore: DateTime.UtcNow,
                audience: _configuration.GetSection("JWT:ValidAudience").Value,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("JWT:DurationInMinutes").Value)),
                claims: userClaims,
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            return (new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), jwtSecurityToken.ValidTo);
        }

        private static List<Claim> BuildUserClaims(User user, IList<string> userRoles, IList<Claim> claims)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName } {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roleClaim = userRoles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            userClaims.AddRange(roleClaim);

            var userClaim = claims.Select(x => new Claim(x.Type, x.Value)).ToList();
            userClaims.AddRange(userClaim);

            return userClaims;
        }

        private string BuildRefreshToken()
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[32];

                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async System.Threading.Tasks.Task SaveRefreshToken(RefreshToken model)
        {
            _context.RefreshTokens.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<ResultModel<JwtResponseDTO>> RefreshToken(string AccessToken, string RefreshToken)
        {
            ClaimsPrincipal claimsPrincipal = GetPrincipalFromToken(AccessToken);

            if (claimsPrincipal == null)
            {
                return new ResultModel<JwtResponseDTO>(ResponseMessage.ErrorMessage000, ApiResponseCode.NOT_FOUND);
            }

            var userId = Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ResultModel<JwtResponseDTO>(ResponseMessage.ErrorMessage000, ApiResponseCode.NOT_FOUND);
            }

            var OldToken = await GetRefreshToken(user.Id, RefreshToken);
            if (OldToken == null)
            {
                return new ResultModel<JwtResponseDTO>(ResponseMessage.ErrorMessage500, ApiResponseCode.NOT_FOUND);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var (token, expiration) = CreateJwtTokenAsync(user, userRoles, userClaims);

            var refreshToken = BuildRefreshToken();
            await RemoveRefreshToken(OldToken);

            await SaveRefreshToken(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration.GetSection("JWT:RefreshTokenExpiration").Value))
            });

            var data = new JwtResponseDTO()
            {
                Token = token,
                Expiration = expiration,
                Roles = userRoles,
                //Permissions = claims.Select(x => x.Value).ToList(),
                RefreshToken = refreshToken
            };

            return new ResultModel<JwtResponseDTO>(data);
        }


        private async Task<RefreshToken> GetRefreshToken(Guid UserId, string refreshToken)
        {
            return await _context.RefreshTokens
                .Where(f => f.UserId == UserId && f.Token == refreshToken && f.ExpiresAt >= DateTime.Now)
                .FirstOrDefaultAsync();
        }


        private async System.Threading.Tasks.Task RemoveRefreshToken(RefreshToken model)
        {
            _context.RefreshTokens.Remove(model);
            await _context.SaveChangesAsync();
        }


        public async Task<ResultModel<RegisterUserDTO>> Register(RegisterUserDTO model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress.ToLower(),
                UserName = model.EmailAddress.ToLower(),
                EmailConfirmed = false,
                PhoneNumber = model.PhoneNumber.ToLower(),
                UserType = UserType.ApplicationUser,
                CreationTime = DateTime.Now,
            };

            var createUserResponse = await _userManager.CreateAsync(user, model.Password);

            if (!createUserResponse.Succeeded)
            {
                return new ResultModel<RegisterUserDTO>(ResponseMessage.AccountCreationFailure, ApiResponseCode.INVALID_REQUEST);
            };

            return new ResultModel<RegisterUserDTO>(model, ResponseMessage.AccountCreationSuccess);
        }


        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenValidator = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            try
            {
                var principal = tokenValidator.ValidateToken(token, parameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Token validation failed: {ex.Message}");
                return null;
            }
        }
    }
}
