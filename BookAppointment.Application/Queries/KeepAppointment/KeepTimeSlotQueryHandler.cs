using BookAppointment.Infrastructure.Models;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace BookAppointment.Application.Queries.KeepAppointment
{
    public class KeepTimeSlotQueryHandler : IRequestHandler<KeepTimeSlotQuery, DateTime>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public KeepTimeSlotQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<DateTime> Handle(KeepTimeSlotQuery keepTimeSlotQuery,
        CancellationToken cancellationToken)
        {
            var appointmentToBook = await _appointmentRepository.GetAppointmentsByTimeAsync(keepTimeSlotQuery.BookingDate, keepTimeSlotQuery.BookingTime);
            var currentDate = keepTimeSlotQuery.BookingDate;
            appointmentToBook.ToList().ForEach(appointment =>
            {
                if (appointment.BookingDate != currentDate){
                    return;
                }
                else{
                    currentDate = currentDate.AddDays(1);
                }
            });

            var newAppointment = new Appointment
            {
                BookingId = Guid.NewGuid(),
                BookingDate = currentDate,
                BookingTime = keepTimeSlotQuery.BookingTime
            };
            await _appointmentRepository.AddAppointment(newAppointment);
            return currentDate;
        }
    }
}
