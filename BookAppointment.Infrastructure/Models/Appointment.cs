namespace BookAppointment.Infrastructure.Models
{
    public class Appointment
    {
        public required DateTime BookingDate { get; init; }
        public required TimeSpan BookingTime { get; init; }
        public required Guid BookingId { get; init; }
        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = "System";
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Active;
    }
}
