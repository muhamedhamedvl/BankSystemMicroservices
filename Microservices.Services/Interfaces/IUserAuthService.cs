using Microservices.Core.Dtos;
using Microservices.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task<bool> IsValidUserAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
        Task RegisterUserAsync(string username, string email, string password);
        Task<string> GenerateJwtTokenAsync(string username);
        Task<string> RefreshJwtTokenAsync(string token);
        Task InvalidateJwtTokenAsync(string token);
        Task<UserAuth?> GetUserWithRolesAsync(string username);
        Task<UserAuth?> GetUserByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<IdentityResult> ForgotPasswordAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal user);
        Task<UserDto> UpdateProfileAsync(string userId, ChangInfoDto dto);

    }

}

