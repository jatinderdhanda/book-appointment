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
                .HasColumnName("booking_id")
                .IsRequired();

            builder.Property(k => k.CreatedBy)
                .HasColumnName("created_by")
                .IsRequired();

            builder.Property(k => k.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(k => k.ModifiedBy)
                .HasColumnName("modified_by")
                .IsRequired();

            builder.Property(k => k.ModifiedDate)
                .HasColumnName("modified_date")
                .IsRequired();

            builder.Property(k => k.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(k => k.BookingTime)
                .HasColumnName("booking_time")
                .IsRequired();

            builder.Property(k => k.BookingDate)
                .HasColumnName("booking_date")
                .IsRequired();
        }
    }
}
