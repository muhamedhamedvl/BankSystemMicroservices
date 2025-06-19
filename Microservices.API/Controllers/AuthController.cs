using Microservices.Core.Dtos;
using Microservices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Microservices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public AuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userAuthService.RegisterUserAsync(dto.Username, dto.Email, dto.Password);
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValid = await _userAuthService.IsValidUserAsync(dto.Username, dto.Password);
            if (!isValid)
                return Unauthorized(new { message = "Invalid username or password." });

            var token = await _userAuthService.GenerateJwtTokenAsync(dto.Username);
            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            try
            {
                var newToken = await _userAuthService.RefreshJwtTokenAsync(dto.OldToken);
                return Ok(new { token = newToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            try
            {
                await _userAuthService.InvalidateJwtTokenAsync(dto.Token);
                return Ok(new { message = "Token invalidated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
                return BadRequest("Invalid email confirmation request.");

            var result = await _userAuthService.ConfirmEmailAsync(userId, token);
            if (!result.Succeeded)
                return BadRequest("Email confirmation failed.");

            return Ok("Email confirmed successfully.");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userDto = await _userAuthService.GetCurrentUserAsync(User);
            if (userDto == null)
                return NotFound("User not found.");

            return Ok(userDto);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userAuthService.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
                return Ok("Password changed successfully.");

            return BadRequest("Password change failed.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var result = await _userAuthService.ForgotPasswordAsync(model.Email);
            if (result.Succeeded)
                return Ok("Password reset link sent.");

            return BadRequest("Error while sending the reset link.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _userAuthService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
            if (result.Succeeded)
                return Ok("Password has been reset successfully.");

            return BadRequest("Error while resetting the password.");
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ChangInfoDto model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var updatedUser = await _userAuthService.UpdateProfileAsync(userId, model);
            return Ok(updatedUser);
        }
    }
}

