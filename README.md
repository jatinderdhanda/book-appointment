#Instructions TO RUN the solution.

1. Update the connection string in AppointmentDBContext.cs file in BookAppointment.Infrastructure project.
2. Create new database and name it BookingAppointment.
3. Execute the sql query to create appointment table, query is added in the Migration Folder.
4. Once your table is created under BookingAppointment database, run the console application.
5. You will see the window with instructions on how to use ADD, DELETE, FIND and KEEP command.

# AREAS covered in the solution.

This project includes functional version of console command line application for calendar booking.

1. Created a separate relational database with required entities,indexes.
2. Implemented CQRS + mediatr pattern and followed sOLID principles.
3. Implemented solution following clean Arctitecture principles. 
4. Implented Unit test cases.


# Areas of Improvements

1. Error Handling: Implement more specific and structured error handling throughout the application. Instead of throwing generic exceptions, consider creating custom exception types for different error scenarios to provide better context to the caller.

2. PipelineBehavior: Implement mediatr pipeline behavior to define generic behavior for all handlers to handle error, validation and logging.

3. Input Validation: Enhance input validation to ensure that all user inputs are validated properly to prevent unexpected behavior or security vulnerabilities. Validate input formats, ranges, and constraints to ensure data integrity.

4. Unit Testing: Increase test coverage by writing more comprehensive unit tests.

5. Use of Cloud platform and services for hosting

6. Use of front-end technology for UI experience

