using AutoFixture;
using BookAppointment.Application.Queries.FindAppointment;
using BookAppointment.Infrastructure.Queries.FindAppointment;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookAppointment.UnitTest
{
    public class FindFreeTimeSlotQueryHandlerTests
    {
        private readonly FindFreeTimeSlotQueryHandler _handler;
        private readonly IAppointmentRepository _appointmentRepository = Substitute.For<IAppointmentRepository>();
        private readonly Fixture _fixture = new();
        public FindFreeTimeSlotQueryHandlerTests()
        {
            _handler = new(_appointmentRepository);
        }

        [Fact]
        public async Task Handle_AppointmentsDoesNotExist_ReturnsOk()
        {
            //Arrange
            var query = _fixture.Create<FindFreeTimeSlotQuery>();
            _appointmentRepository.GetAppointments(Arg.Any<DateTime>()).Returns(new List<TimeSpan>());

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_AppointmentsDoesnExist_ReturnsNextAvailableFreeSlot()
        {
            //Arrange
            var query = _fixture.Build<FindFreeTimeSlotQuery>().With(x => x.BookingDate, DateTime.UtcNow).Create();
            _appointmentRepository.GetAppointments(Arg.Any<DateTime>()).Returns(new List<TimeSpan> { TimeSpan.FromHours(9.5)});

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Value.Hour.Should().Be(9);
        }
    }
}
