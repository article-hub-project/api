using BLL.Entities;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(JwtConfiguration configuration, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> RegisterAsync(string email, string password, string username)
        {
            var user = new User()
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Username = username
            };

            await _userRepository.CreateAsync(user);
            if (string.IsNullOrEmpty(user.Id))
                return null;

            return GenerateToken(user);
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return null;
            else if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return GenerateToken(user);
        }

        public string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(Constants.UserIdClaim)?.Value;
        }

        public async Task<User> GetCurrentUserProfileAsync()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return null;

            return await _userRepository.GetByIdAsync(userId);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Constants.UserIdClaim, user.Id)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
