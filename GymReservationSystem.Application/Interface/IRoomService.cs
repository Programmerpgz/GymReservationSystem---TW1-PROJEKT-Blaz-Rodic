using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto?> GetByIdAsync(int id);
        Task<RoomDto> CreateAsync(RoomDto dto);
        Task<RoomDto> UpdateAsync(int id, RoomDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
