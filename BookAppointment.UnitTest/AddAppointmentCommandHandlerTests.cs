using AutoFixture;
using NSubstitute;
using BookAppointment.Infrastructure.Commands.AddApponitment;
using BookAppointment.Infrastructure.Repositories.Interfaces;
using BookAppointment.Infrastructure.Models;
using FluentAssertions;
using Xunit;

namespace BookAppointment.UnitTest
{
    public class AddAppointmentCommandHandlerTests
    {
        private readonly AddAppointmentCommandHandler _handler;
        private readonly IAppointmentRepository _appointmentRepository = Substitute.For<IAppointmentRepository>();
        private readonly Fixture _fixture = new();
        public AddAppointmentCommandHandlerTests()
        {
            _handler = new(_appointmentRepository);
        }

        [Fact]
        public async Task Handle_AddNewAppointment_ReturnsTrue()
        {
            //Arrange
            var command = _fixture.Create<AddAppointmentCommand>();
            _appointmentRepository.AddAppointment(Arg.Any<Appointment>()).Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
        }
    }
}
