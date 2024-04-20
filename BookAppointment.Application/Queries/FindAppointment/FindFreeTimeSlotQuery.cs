using MediatR;

namespace BookAppointment.Infrastructure.Queries.FindAppointment
{
    public class FindFreeTimeSlotQuery : IRequest<DateTime?>
    {
        public required DateTime BookingDate { get; init; }
    }
}
