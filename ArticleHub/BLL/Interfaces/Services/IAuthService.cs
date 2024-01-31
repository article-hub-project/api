﻿using BLL.Entities;

namespace BLL.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(string email, string password);
        Task<string?> LoginAsync(string email, string password);
    }
}
