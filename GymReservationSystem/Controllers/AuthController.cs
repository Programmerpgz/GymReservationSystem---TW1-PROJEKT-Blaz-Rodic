using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using Microsoft.AspNetCore.Mvc;
namespace GymReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            _logger.LogInformation("Login pokušaj za {Email}", dto.Email);
            try
            {
                var result = await _service.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            _logger.LogInformation("Registracija novog korisnika {Email}", dto.Email);

            try
            {
                var result = await _service.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500
                };
            }
        }
    }
}
