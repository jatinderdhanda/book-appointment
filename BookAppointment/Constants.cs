namespace BookAppointment
{
    public class Constants
    {
        public const string AddCommand = "ADD";
        public const string DeleteCommand = "DELETE";
        public const string FindCommand = "FIND";
        public const string KeepCommand = "KEEP";
        public const string BookingSlotUnAvailable = "Booking cannot be made beacuse selected slot is not available";
        public const string InvalidCommandErrorMessage = "Invalid command. Please try again.";
        public const string AddCommandSuccessMessage = "Appointment added successfully";
        public const string DeleteCommandSuccessMessage = "Appointment deleted successfully";
        public const string FailedCommandMessage = "Performed action was unsuccessful";
        public const string KeepCommandSuccessMessage = "kept timeslot for the day is:";
        public const string FindCommandSuccessMessage = "Next available free timeslot for the day is:";
        public const string FindCommandErrorMessage = "Unable to find any available time slot for the given date";
        public const string InvalidCommandFormatForAddDelete = "Invalid command format. Usage: ADD DD/MM hh:mm";
        public const string InvalidCommandFormatForFind = "Invalid command format. Usage: FIND DD/MM";
        public const string InvalidCommandFormatForKeep = "Invalid command format. Usage: KEEP hh:mm";
        public const string InvalidDateTimeFormat = "Invalid date/time format. Usage: ADD DD/MM hh:mm";
        public const string InvalidDateFormat = "Invalid date format. Usage: FIND DD/MM";
        public const string InvalidTimeFormat = "Invalid time format. Usage: KEEP hh:mm";
    }
}
