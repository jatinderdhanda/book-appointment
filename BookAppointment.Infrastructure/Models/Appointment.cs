namespace BookAppointment.Infrastructure.Models
{
    public class Appointment
    {
        public required DateTime BookingDate { get; init; }
        public required TimeSpan BookingTime { get; init; }
        public required Guid BookingId { get; init; }
    }
}
