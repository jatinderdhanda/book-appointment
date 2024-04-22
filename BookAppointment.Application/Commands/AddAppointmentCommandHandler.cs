using BookAppointment.Infrastructure.Models;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace BookAppointment.Infrastructure.Commands.AddApponitment
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AddAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<bool> Handle(AddAppointmentCommand command,
        CancellationToken cancellationToken)
        {
            var existingAppointment = await _appointmentRepository.GetAppointments(command.BookingDate);
            var result = true;
            existingAppointment.ToList().ForEach(bookingTime =>
            {
                if(bookingTime <= command.BookingTime && bookingTime.Add(TimeSpan.FromMinutes(30)) > command.BookingTime)
                {
                    result = false;
                    return;
                }
            });
            if (result)
            {
                var appointment = new Appointment
                {
                    BookingId = Guid.NewGuid(),
                    BookingDate = command.BookingDate,
                    BookingTime = command.BookingTime
                };
                await _appointmentRepository.AddAppointment(appointment);
            }
            return result;
        }
    }
}
