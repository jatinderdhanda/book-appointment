using BookAppointment.Application.Queries.KeepAppointment;
using BookAppointment.Infrastructure.Queries.FindAppointment;
using MediatR;
using System.Globalization;

namespace BookAppointment
{
    public static class ProgramExtension
    {
        public static async Task ProcessAddCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null)
            {
                return;
            }
            _ = await mediator.Send(new AddAppointmentCommand
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });
        }

        public static async Task ProcessDeleteCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null)
            {
                return;
            }
            _ = await mediator.Send(new DeleteAppointmentCommand
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });
        }

        public static async Task ProcessFindCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null)
            {
                return;
            }
            var result = await mediator.Send(new FindFreeTimeSlotQuery
            {
                BookingDate = bookingDateTime.Value.Date
            });

            Console.WriteLine("Next available free timeslot for the day is:" + result);
        }

        public static async Task ProcessKeepCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null)
            {
                return;
            }
            var result = await mediator.Send(new KeepTimeSlotQuery
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });

            Console.WriteLine("Next available free timeslot for the day is:" + result);
        }

        static DateTime? GetBookingDateTime(string[] commandParts)
        {
            string commandType = commandParts[0];

            switch (commandType)
            {

                case "ADD":
                case "DELETE":
                    if (commandParts.Length != 3)
                    {
                        Console.WriteLine("Invalid command format. Usage: ADD DD/MM hh:mm");
                        return null;
                    }
                    string dateTimeString = commandParts[1] + " " + commandParts[2];
                    if (DateTime.TryParseExact(dateTimeString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                    {
                        return dateTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date/time format. Usage: ADD DD/MM hh:mm");
                    }
                    return null;
                case "FIND":
                    if (commandParts.Length != 2)
                    {
                        Console.WriteLine("Invalid command format. Usage: FIND DD/MM");
                        return null;
                    }

                    if (DateTime.TryParseExact(commandParts[1], "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate))
                    {
                        return selectedDate;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Usage: FIND DD/MM");
                    }
                    return null;
                case "KEEP":

                    if (commandParts.Length != 2)
                    {
                        Console.WriteLine("Invalid command format. Usage: KEEP hh:mm");
                        return null;
                    }

                    if (DateTime.TryParseExact(commandParts[1], "hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedTime))
                    {
                        return selectedTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid time format. Usage: KEEP hh:mm");
                    }
                    return null;
            }
            return null;
        }
    }
}
