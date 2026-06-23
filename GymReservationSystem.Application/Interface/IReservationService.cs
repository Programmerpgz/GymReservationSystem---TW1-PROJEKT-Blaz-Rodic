using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> GetByIdAsync(int id);
        Task<ReservationDto> CreateAsync(ReservationDto dto);
        Task<ReservationDto> UpdateAsync(int id, ReservationDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ReservationDto>> GetByUserIdAsync(int userId);
    }
}
