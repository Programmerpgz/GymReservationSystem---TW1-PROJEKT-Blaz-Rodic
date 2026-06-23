using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerDto>> GetAllAsync();
        Task<TrainerDto?> GetByIdAsync(int id);
        Task<TrainerDto> CreateAsync(TrainerDto dto);
        Task<TrainerDto> UpdateAsync(int id, TrainerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
