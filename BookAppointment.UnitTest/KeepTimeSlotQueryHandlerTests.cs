using AutoFixture;
using BookAppointment.Application.Queries.KeepAppointment;
using BookAppointment.Infrastructure.Models;
using BookAppointment.Infrastructure.Queries.FindAppointment;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookAppointment.UnitTest
{
    public class KeepTimeSlotQueryHandlerTests
    {
        private readonly KeepTimeSlotQueryHandler _handler;
        private readonly IAppointmentRepository _appointmentRepository = Substitute.For<IAppointmentRepository>();
        private readonly Fixture _fixture = new();

        public KeepTimeSlotQueryHandlerTests()
        {
            _handler = new(_appointmentRepository);
        }

        [Fact]
        public async Task Handle_KeepTimeSlotShouldReturnCurrentBookingDate()
        {
            //Arrange
            var query = _fixture.Create<KeepTimeSlotQuery>();
            _appointmentRepository.GetAppointmentsByTimeAsync(Arg.Any<DateTime>(), Arg.Any<TimeSpan>()).Returns(new List<Appointment>());

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().Be(query.BookingDate);
        }

        [Fact]
        public async Task Handle_AppointmentsDoesnExist_ReturnsNextAvailableFreeSlot()
        {
            //Arrange
            var currentDate = DateTime.UtcNow;
            var modifiedDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0);
            var query = _fixture.Build<KeepTimeSlotQuery>()
                .With(x => x.BookingDate, modifiedDate)
                .With(x => x.BookingTime, TimeSpan.FromHours(9)).Create();

            _appointmentRepository.GetAppointmentsByTimeAsync(Arg.Any<DateTime>(), Arg.Any<TimeSpan>()).Returns(new List<Appointment>()
            {
                new Appointment
                {
                BookingDate = modifiedDate,
                BookingTime = TimeSpan.FromHours(9),
                BookingId = Guid.NewGuid()
                }
            });

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Date.Should().Be(query.BookingDate.AddDays(1).Date);
        }
    }
}
