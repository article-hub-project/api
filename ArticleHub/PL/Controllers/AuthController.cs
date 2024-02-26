using AutoMapper;
using BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.ViewModels.Article;
using PL.ViewModels.Auth;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterViewModel model)
        {
            var accessToken = await _authService.RegisterAsync(model.Email, model.Password, model.Username);
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

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<string>> Profile()
        {
            var user = await _authService.GetCurrentUserProfileAsync();
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserViewModel>(user));
        }
    }
}
