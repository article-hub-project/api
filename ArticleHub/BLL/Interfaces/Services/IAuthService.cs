using BLL.Entities;

namespace BLL.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(string email, string password, string username);
        Task<string?> LoginAsync(string email, string password);
        string? GetCurrentUserId();
        Task<User> GetCurrentUserProfileAsync();
    }
}
