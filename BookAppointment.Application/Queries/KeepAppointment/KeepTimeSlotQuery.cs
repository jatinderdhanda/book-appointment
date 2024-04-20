using MediatR;

namespace BookAppointment.Application.Queries.KeepAppointment
{
    public class KeepTimeSlotQuery: IRequest<DateTime>
    {
        public required DateTime BookingDate { get; init; }
        public required TimeSpan BookingTime { get; init; }
    }
}
