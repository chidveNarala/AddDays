using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace AddDays;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        ServiceProvider serviceProvider = new ServiceCollection()
                .AddSingleton<IDateService, DateService>().BuildServiceProvider();

        var dateService = serviceProvider.GetService<IDateService>();

        bool continueRunning = true;

        while (continueRunning)
        {
            Console.WriteLine("Enter the date (dd/mm/yyyy): ");
            var inputDate = Console.ReadLine();

            Console.WriteLine("Enter the number of days to add: ");
            var daysToAdd = int.Parse(Console.ReadLine() ?? string.Empty);

            var newDate = dateService!.AddDaysToDate(inputDate, daysToAdd);
            Console.WriteLine($"New Date: {newDate}");

            Console.WriteLine("Do you want to enter another date? (y/n)");
            var response = Console.ReadLine();

            if (response != "y")
            {
                continueRunning = false;
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}