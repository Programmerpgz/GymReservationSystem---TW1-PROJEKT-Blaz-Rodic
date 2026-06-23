namespace GymReservationSystem.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; } = "Confirmed";
        public DateTime CreatedAt { get; set; }
        public User User { get; set; } = null!;
        public Training Training { get; set; } = null!;
    }
}