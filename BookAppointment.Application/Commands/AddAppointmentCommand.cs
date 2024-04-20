using MediatR;

public class AddAppointmentCommand : IRequest<bool>
{
    public required DateTime BookingDate { get; init; }
    public required TimeSpan BookingTime { get; init; }
}