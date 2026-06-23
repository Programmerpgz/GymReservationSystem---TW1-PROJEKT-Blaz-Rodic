namespace GymReservationSystem.Application.DTOs
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public int DurationMinutes { get; set; }
        public int MaxParticipants { get; set; }
        public int TrainerId { get; set; }
        public int RoomId { get; set; }
        public bool IsActive { get; set; }
    }
}