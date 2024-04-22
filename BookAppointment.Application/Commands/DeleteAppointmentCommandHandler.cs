using MediatR;
using BookAppointment.Infrastructure.Repositories.Interfaces;

namespace BookAppointment.Infrastructure.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<bool> Handle(DeleteAppointmentCommand command,
        CancellationToken cancellationToken)
        {
            var appointmentToDelete = await _appointmentRepository.GetAppointmentAsync(command.BookingDate, command.BookingTime);
            if (appointmentToDelete == null)
            {
                return false;
            }
            await _appointmentRepository.UpdateAppointmentAsync(appointmentToDelete);
            return true;
        }
    }
}
