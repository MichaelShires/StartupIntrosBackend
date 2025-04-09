
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Spectre.Console;
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
        Spectre.Console.AnsiConsole.Clear();
        Spectre.Console.AnsiConsole.MarkupLine("[bold blue]=== Startup Intros Backend Console App ===[/]");
        var option = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
            .Title("[green]Select an option:[/]")
            .PageSize(10)
            .AddChoices(new[] {
              "Add News Source",
              "Process all News Sources",
              "Exit"
            }));
        switch (option)
        {
          case "Add News Source":
            await _newsSourceService.AddNewsSourceInteractiveAsync();
            break;
          case "Process all News Sources":
            await _dbService.ProcessAllNewsFeedsAsync();
            Spectre.Console.AnsiConsole.MarkupLine("[green]Processed all news feeds successfully.[/]");
            break;
          case "Exit":
            exit = true;
            break;
          default:
            Spectre.Console.AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
            break;
        }
        Spectre.Console.AnsiConsole.WriteLine("\n Press any key to continue . . .");
        Console.ReadKey(true);
      }
    }
  }
}