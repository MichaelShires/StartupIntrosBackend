
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using StartupIntrosBackend.Data;

namespace StartupIntrosBackend.Services
{
  public class ConsoleMenuService
  {
    private readonly DbService _dbService;
    private readonly NewsSourceService _newsSourceService;

    public ConsoleMenuService(DbService dbService)
    {
      _dbService = dbService;
      // Initialize the service that handles news source operations.
      _newsSourceService = new NewsSourceService(_dbService);
    }

    public async Task RunMenuAsync()
    {
      bool exit = false;
      while (!exit)
      {
        Console.WriteLine();
        Console.WriteLine("Select an option:");
        Console.WriteLine("1: Add News Source");
        Console.WriteLine("2: Process All News Feeds");
        Console.WriteLine("3: Exit");
        Console.Write("Enter your choice: ");

        var choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
          case "1":
            await _newsSourceService.AddNewsSourceInteractiveAsync();
            break;
          case "2":
            await _dbService.ProcessAllNewsFeedsAsync();
            Console.WriteLine("Processed all news feeds successfully.");
            break;
          case "3":
            exit = true;
            break;
          default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
        }
      }
    }
  }
}