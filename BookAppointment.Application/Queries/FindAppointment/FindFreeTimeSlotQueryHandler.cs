using BookAppointment.Infrastructure.Queries.FindAppointment;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace BookAppointment.Application.Queries.FindAppointment
{
    public class FindFreeTimeSlotQueryHandler : IRequestHandler<FindFreeTimeSlotQuery, DateTime?>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public FindFreeTimeSlotQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<DateTime?> Handle(FindFreeTimeSlotQuery query, CancellationToken cancellationToken)
        {
            var selectedDate = query.BookingDate;
            var existingAppointments = await _appointmentRepository.GetAppointments(query.BookingDate);

            if (!existingAppointments.Any())
            {
                return null;
            }
            var startTime = selectedDate.Date.AddHours(9); // Start at 9 AM
            var endTime = selectedDate.Date.AddHours(17); // End at 5 PM
            int weekOfMonth = (selectedDate.Day - 1) / 7 + 1;

            while (startTime < endTime)
            {
                if (weekOfMonth == 3 && selectedDate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    if (startTime.Hour == 16 && startTime.Minute == 0)
                    {
                        return null;
                    }
                }
                // Check if the current time slot is available
                if (!existingAppointments.Any(bookedSlot => bookedSlot == startTime.TimeOfDay))
                {
                    return startTime;
                }

                // Move to the next time slot (30-minute interval)
                startTime = startTime.AddMinutes(30);
            }
            return null;
        }
    }
}
