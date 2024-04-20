using BookAppointment;
using BookAppointment.Application;
using BookAppointment.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // Setup dependency injection
        var serviceProvider = new ServiceCollection()
            .AddDatabase()
            .AddApplication()
            .BuildServiceProvider();

        // Resolve the mediator
        var mediator = serviceProvider.GetService<IMediator>();

        while (true)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("ADD DD/MM hh:mm - Add an appointment");
            Console.WriteLine("DELETE DD/MM hh:mm - Remove an appointment");
            Console.WriteLine("FIND DD/MM - Find a free timeslot for the day");
            Console.WriteLine("KEEP hh:mm - Keep a timeslot for any day");
            string input = Console.ReadLine().Trim().ToUpper();

            if (input == "EXIT")
            {
                break; // Exit the loop and end the program
            }

            string[] commandParts = input.Split(' ');
            string commandType = commandParts.FirstOrDefault();

            switch (commandType)
            {
                case "ADD":
                    await ProgramExtension.ProcessAddCommand(commandParts, mediator);
                    break;
                case "DELETE":
                    await ProgramExtension.ProcessDeleteCommand(commandParts, mediator);
                    break;
                case "FIND":
                    await ProgramExtension.ProcessFindCommand(commandParts, mediator);
                    break;
                case "KEEP":
                    await ProgramExtension.ProcessKeepCommand(commandParts, mediator);
                    break;
                default:
                    Console.WriteLine("Invalid command. Please try again.");
                    break;
            }
        }
    }
}