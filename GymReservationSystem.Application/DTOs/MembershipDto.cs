namespace GymReservationSystem.Application.DTOs
{
    public class MembershipDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}