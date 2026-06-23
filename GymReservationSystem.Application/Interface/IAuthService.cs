using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    }
}
