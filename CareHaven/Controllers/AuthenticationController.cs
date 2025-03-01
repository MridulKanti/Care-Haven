
using System.Threading.Tasks;
using CareHaven.Services;
using CareHaven.Models;
using CareHaven.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CareHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                //if (await _authService.UserExists(userDTO.Email))
                //    return BadRequest("User already exists");

                var result = await _authService.Register(userDTO, userDTO.UserRole);

                if (result.Item1 == 1)
                    return Ok(new { message = result.Item2 });

                return BadRequest(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register user");
                return StatusCode(500, "Failed to register user" + ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var result = await _authService.Login(model);

                if (result.Item1 == 1)
                    return Ok(new { token = result.Item2 });

                return Unauthorized(result.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to login user");
                return StatusCode(500, "Failed to login user");
            }
        }
    }
}