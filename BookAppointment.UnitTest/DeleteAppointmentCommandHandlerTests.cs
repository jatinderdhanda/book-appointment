using AutoFixture;
using BookAppointment.Infrastructure.Commands.DeleteAppointment;
using BookAppointment.Infrastructure.Models;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BookAppointment.UnitTest
{
    public class DeleteAppointmentCommandHandlerTests
    {
        private readonly DeleteAppointmentCommandHandler _handler;
        private readonly IAppointmentRepository _appointmentRepository = Substitute.For<IAppointmentRepository>();
        private readonly Fixture _fixture = new();
       
        public DeleteAppointmentCommandHandlerTests()
        {
            _handler = new(_appointmentRepository);
        }

        [Fact]
        public async Task Handle_AppointmentDoesnnotExist_ThrowsException()
        {
            //Arrange
            var command = _fixture.Create<DeleteAppointmentCommand>();
            _ = _appointmentRepository.GetAppointmentAsync(Arg.Any<DateTime>(), Arg.Any<TimeSpan>()).ReturnsForAnyArgs((Appointment)null);
            
            //Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_AppointmentDoesExist_ReturnsResult()
        {
            //Arrange
            var command = _fixture.Create<DeleteAppointmentCommand>();
            var appointment = _fixture.Create<Appointment>();
            _appointmentRepository.GetAppointmentAsync(Arg.Any<DateTime>(), Arg.Any<TimeSpan>()).Returns(appointment);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
        }
    }
}