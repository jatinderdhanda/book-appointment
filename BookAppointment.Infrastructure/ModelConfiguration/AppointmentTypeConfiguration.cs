using BookAppointment.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookAppointment.Infrastructure.ModelConfiguration
{
    public class AppointmentTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointment")
                .HasKey(k => k.BookingId);

            builder.Property(k => k.BookingId)
                .HasColumnName("booking_id");

            builder.Property(k => k.BookingTime)
                .HasColumnName("booking_time")
                .IsRequired();

            builder.Property(k => k.BookingDate)
                .HasColumnName("booking_date")
                .IsRequired();
        }
    }
}
