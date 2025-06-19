using Microservices.Core.Models;
using Microservices.Repository.Data;
using Microservices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microservices.Core.Dtos;

public class UserAuthRepository : IUserAuthRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHasher<UserAuth> _passwordHasher;

    public UserAuthRepository(ApplicationDbContext context, IConfiguration configuration, IPasswordHasher<UserAuth> passwordHasher)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> IsValidUserAsync(string username, string password)
    {
        var user = await _context.UserAuths.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return false;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _context.UserAuths.AnyAsync(u => u.Username == username);
    }

    public async Task RegisterUserAsync(string username, string password, string email)
    {
        if (await UserExistsAsync(username))
        {
            throw new Exception("User already exists.");
        }

        var user = new UserAuth
        {
            Username = username,
            Email = email
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
        if (defaultRole != null)
        {
            user.UserRoles.Add(new UserRole { Role = defaultRole });
        }

        await _context.UserAuths.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GenerateJwtTokenAsync(string username)
    {
        var user = await GetUserWithRolesAsync(username);
        if (user == null)
            throw new Exception("User not found");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> RefreshJwtTokenAsync(string oldToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(oldToken);

        var username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(username) || !await UserExistsAsync(username))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return await GenerateJwtTokenAsync(username);
    }

    public Task InvalidateJwtTokenAsync(string token)
    {
        throw new NotImplementedException("Token invalidation not implemented yet.");
    }

    public async Task<UserAuth?> GetUserWithRolesAsync(string username)
    {
        return await _context.UserAuths
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task<UserAuth?> GetUserByIdAsync(string userId)
    {
        if (int.TryParse(userId, out int id))
        {
            return await _context.UserAuths
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        return null;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _context.UserAuths.FindAsync(int.Parse(userId));
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        if (user.EmailConfirmed)
            return IdentityResult.Failed(new IdentityError { Description = "Email already confirmed." });

        var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        if (decodedToken != user.Email)
            return IdentityResult.Failed(new IdentityError { Description = "Invalid token." });

        user.EmailConfirmed = true;
        _context.UserAuths.Update(user);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ForgotPasswordAsync(string email)
    {
        var user = await _context.UserAuths.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _context.UserAuths.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        if (decodedToken != user.Email)
            return IdentityResult.Failed(new IdentityError { Description = "Invalid token." });

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
        _context.UserAuths.Update(user);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
        if (result != PasswordVerificationResult.Success)
            return IdentityResult.Failed(new IdentityError { Description = "Incorrect current password." });

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
        _context.UserAuths.Update(user);
        await _context.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await GetUserByIdAsync(userId!);

        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = user.UserRoles.Select(r => r.Role.Name).ToList()
        };
    }

    public async Task<UserDto?> UpdateProfileAsync(string userId, ChangInfoDto model)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null)
            return null;

        user.Username = model.Username;
        user.Email = model.Email;

        _context.UserAuths.Update(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Roles = user.UserRoles.Select(r => r.Role.Name).ToList()
        };
    }
}