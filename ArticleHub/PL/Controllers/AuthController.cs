using BLL.Entities;
using BLL.Interfaces.Services;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PL.ViewModels.Auth;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService context)
        {
            _authService = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterViewModel model)
        {
            var accessToken = await _authService.RegisterAsync(model.Email, model.Password);
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest();

            return Ok(accessToken);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginViewModel model)
        {
            var accessToken = await _authService.LoginAsync(model.Email, model.Password);
            if (string.IsNullOrEmpty(accessToken))
                return BadRequest();

            return Ok(accessToken);
        }
    }
}
