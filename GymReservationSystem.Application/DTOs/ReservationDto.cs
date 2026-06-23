namespace GymReservationSystem.Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } = "Confirmed";
        public DateTime CreatedAt { get; set; }
    }
}