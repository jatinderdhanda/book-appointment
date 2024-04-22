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
            if (bookingDateTime == null || !CheckTimeIsInRange(bookingDateTime.Value.TimeOfDay) || CheckIfDateIsThirdWeekTuesday(bookingDateTime.Value.Date))
            {
                Console.WriteLine(Constants.BookingSlotUnAvailable);
                return;
            }
            var result = await mediator.Send(new AddAppointmentCommand
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });

            if (result)
            {
                Console.WriteLine(Constants.AddCommandSuccessMessage);
            }
            else
            {
                Console.WriteLine(Constants.FailedCommandMessage);
            }
        }

        public static async Task ProcessDeleteCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null || !CheckTimeIsInRange(bookingDateTime.Value.TimeOfDay))
            {
                Console.WriteLine(Constants.BookingSlotUnAvailable);
                return;
            }
            var result = await mediator.Send(new DeleteAppointmentCommand
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });

            if (result)
            {
            Console.WriteLine(Constants.DeleteCommandSuccessMessage);
            }
            else
            {
                Console.WriteLine(Constants.FailedCommandMessage);
            }
        }

        public static async Task ProcessFindCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null)
            {
                Console.WriteLine(Constants.BookingSlotUnAvailable);
                return;
            }
            var result = await mediator.Send(new FindFreeTimeSlotQuery
            {
                BookingDate = bookingDateTime.Value.Date
            });
            if (result != null)
            {
                Console.WriteLine(Constants.FindCommandSuccessMessage + result);
            }
            else {
                Console.WriteLine(Constants.FindCommandErrorMessage);
            }
        }

        public static async Task ProcessKeepCommand(string[] commandParts, IMediator mediator)
        {

            var bookingDateTime = GetBookingDateTime(commandParts);
            if (bookingDateTime == null || !CheckTimeIsInRange(bookingDateTime.Value.TimeOfDay))
            {
                Console.WriteLine(Constants.BookingSlotUnAvailable);
                return;
            }
            var result = await mediator.Send(new KeepTimeSlotQuery
            {
                BookingDate = bookingDateTime.Value.Date,
                BookingTime = bookingDateTime.Value.TimeOfDay
            });

            Console.WriteLine(Constants.KeepCommandSuccessMessage + result);
        }

        static DateTime? GetBookingDateTime(string[] commandParts)
        {
            string commandType = commandParts[0];

            switch (commandType)
            {

                case Constants.AddCommand:
                case Constants.DeleteCommand:
                    if (commandParts.Length != 3)
                    {
                        Console.WriteLine(Constants.InvalidCommandFormatForAddDelete);
                        return null;
                    }
                    string dateTimeString = commandParts[1] + " " + commandParts[2];
                    if (DateTime.TryParseExact(dateTimeString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                    {
                        return dateTime;
                    }
                    else
                    {
                        Console.WriteLine(Constants.InvalidDateTimeFormat);
                    }
                    return null;
                case Constants.FindCommand:
                    if (commandParts.Length != 2)
                    {
                        Console.WriteLine(Constants.InvalidCommandFormatForFind);
                        return null;
                    }

                    if (DateTime.TryParseExact(commandParts[1], "dd/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate))
                    {
                        return selectedDate;
                    }
                    else
                    {
                        Console.WriteLine(Constants.InvalidDateFormat);
                    }
                    return null;
                case Constants.KeepCommand:

                    if (commandParts.Length != 2)
                    {
                        Console.WriteLine(Constants.InvalidCommandFormatForKeep);
                        return null;
                    }

                    if (DateTime.TryParseExact(commandParts[1], "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedTime))
                    {
                        return selectedTime;
                    }
                    else
                    {
                        Console.WriteLine(Constants.InvalidTimeFormat);
                    }
                    return null;
            }
            return null;
        }

        static bool CheckTimeIsInRange(TimeSpan bookingTime)
        {
            if(bookingTime.Hours<9) return false;
            var modifiedTime = bookingTime.Add(TimeSpan.FromMinutes(30));
            return modifiedTime.Hours >= 9 && modifiedTime.Hours < 17;
            
        }

        static bool CheckIfDateIsThirdWeekTuesday(DateTime bookingDate)
        {
            var weekOfMonth = (bookingDate.Day - 1) / 7 + 1;
            return  weekOfMonth == 3 && bookingDate.DayOfWeek == DayOfWeek.Tuesday;
        }
    }
}
