using BookAppointment.Infrastructure.ModelConfiguration;
using BookAppointment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAppointment.Infrastructure
{
    public class AppointmentDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=HY-000000005WLI\\SQLSERVER2022;UID=sa;PWD=Dhanda@1991;initial catalog=BookingAppointment;Persist Security Info=True;Encrypt=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasQueryFilter(x => x.Status == Status.Active);
            modelBuilder.ApplyConfiguration(new AppointmentTypeConfiguration());
        }
    }

   
}