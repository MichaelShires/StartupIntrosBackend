using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StartupIntrosBackend.Data;
using StartupIntrosBackend.Models;
using StartupIntrosBackend.Services;

namespace StartupIntrosBackend;

public static class Program
{
  public static async Task Main(string[] args)
  {
    var dbService = DbService.CreateAsync();

    Console.WriteLine("\n\n=== StartupIntrosBackend Console App ===");
    Console.WriteLine("Database has been updated.");

    // Run the interactive menu loop.
    var menuService = new ConsoleMenuService(await dbService);
    await menuService.RunMenuAsync();

    Console.WriteLine("Exiting the application. Goodbye!");
  }
}

/*

https://techcrunch.com/feed/

*/