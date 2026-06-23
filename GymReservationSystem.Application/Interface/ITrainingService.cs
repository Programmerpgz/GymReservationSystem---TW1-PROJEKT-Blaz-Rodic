using GymReservationSystem.Application.DTOs;

namespace GymReservationSystem.Application.Interface
{
    public interface ITrainingService
    {
        Task<IEnumerable<TrainingDto>> GetAllAsync();
        Task<TrainingDto?> GetByIdAsync(int id);
        Task<TrainingDto> CreateAsync(TrainingDto dto);
        Task<TrainingDto> UpdateAsync(int id, TrainingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
