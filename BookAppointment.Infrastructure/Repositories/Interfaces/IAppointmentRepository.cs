using BookAppointment.Infrastructure.Models;

namespace BookAppointment.Infrastructure.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAppointment(Appointment appointment);
        Task RemoveAppointment(Appointment appointment);
        Task<Appointment?> GetAppointmentAsync(DateTime bookingDate, TimeSpan bookingTime);
        Task<IEnumerable<Appointment>> GetAppointmentsByTimeAsync(DateTime bookingDate, TimeSpan bookingTime);
        Task<IEnumerable<TimeSpan>> GetAppointments(DateTime bookingDate);
    }
}
