using MediatR;

public class DeleteAppointmentCommand : IRequest<bool>
{
    public required DateTime BookingDate { get; init; }

    public  required TimeSpan BookingTime { get; init; }
}