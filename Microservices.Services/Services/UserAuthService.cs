using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Services.Interfaces;
using Microservices.Core.Models;
using Microservices.Repository.Interfaces;
using System.Collections.ObjectModel;
using Microservices.Core.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Microservices.Services.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        public UserAuthService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }
        public Task<bool> IsValidUserAsync(string username, string password)
        {
            return _userAuthRepository.IsValidUserAsync(username, password);
        }

        public Task<bool> UserExistsAsync(string username)
        {
            return _userAuthRepository.UserExistsAsync(username);
        }

        public Task RegisterUserAsync(string username, string email, string password)
        {
            return _userAuthRepository.RegisterUserAsync(username, email, password);
        }

        public Task<string> GenerateJwtTokenAsync(string username)
        {
            return _userAuthRepository.GenerateJwtTokenAsync(username);
        }

        public Task<string> RefreshJwtTokenAsync(string token)
        {
            return _userAuthRepository.RefreshJwtTokenAsync(token);
        }

        public Task InvalidateJwtTokenAsync(string token)
        {
            return _userAuthRepository.InvalidateJwtTokenAsync(token);
        }
        public async Task<UserAuth?> GetUserWithRolesAsync(string username)
        {
            return await _userAuthRepository.GetUserWithRolesAsync(username);
        }
        public async Task<UserAuth?> GetUserByIdAsync(string userId)
        {
            return await _userAuthRepository.GetUserByIdAsync(userId);
        }

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            return _userAuthRepository.ConfirmEmailAsync(userId, token);
        }

        public Task<IdentityResult> ForgotPasswordAsync(string email)
        {
            return _userAuthRepository.ForgotPasswordAsync(email);
        }

        public Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            return _userAuthRepository.ResetPasswordAsync(email, token, newPassword);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return _userAuthRepository.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            return _userAuthRepository.GetCurrentUserAsync(user);
        }

        public Task<UserDto> UpdateProfileAsync(string userId, ChangInfoDto dto)
        {
            return _userAuthRepository.UpdateProfileAsync(userId, dto);
        }
    }
}
