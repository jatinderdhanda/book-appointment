using BookAppointment.Infrastructure.Models;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAppointment.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentDbContext _dbContext;
        public AppointmentRepository(AppointmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Appointment?> GetAppointmentAsync(DateTime bookingDate, TimeSpan bookingTime)
        {
            return await _dbContext.Appointments.FirstOrDefaultAsync(x => x.BookingDate == bookingDate && x.BookingTime == bookingTime);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByTimeAsync(DateTime bookingDate,TimeSpan bookingTime)
        {
            return await _dbContext.Appointments.Where(x=> x.BookingDate >= bookingDate && x.BookingTime == bookingTime).OrderBy(x=>x.BookingDate).ToListAsync();
        }

        public async Task<IEnumerable<TimeSpan>> GetAppointments(DateTime bookingDate)
        {
            return await _dbContext.Appointments.Where(x => x.BookingDate == bookingDate).Select(y => y.BookingTime).ToListAsync();
        }

    }
}
