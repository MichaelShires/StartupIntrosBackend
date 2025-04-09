using Microsoft.EntityFrameworkCore;

namespace StartupIntrosBackend;

public class Program
{
  public static void Main(string[] args)
  {
    // Set DataDirectory to the current directory (or another desired path)
    AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseSqlite("Data Source=|DataDirectory|MyDatabase.db")
      .Options;

    // Ensure the folder exists
    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "sqldata");
    if (!Directory.Exists(dataFolder))
    {
      Directory.CreateDirectory(dataFolder);
    }

    using (var context = new AppDbContext(options))
    {
      // Apply any pending migrations. This will create the database if it doesn't exist.
      context.Database.Migrate();

      // Example: add a new source entry
      context.NewsSources.Add(new NewsSource() { Url = "https://example.com/feed" });
      context.SaveChanges();

      // Retrieve and display sources
      foreach (var source in context.NewsSources)
      {
        Console.WriteLine($"Source ID: {source.Id}, URL: {source.Url}");
      }
    }
    
    Console.WriteLine("Current Directory (Environment.CurrentDirectory): " + Environment.CurrentDirectory);
  }
}