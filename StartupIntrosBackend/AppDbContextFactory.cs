using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StartupIntrosBackend;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    // Set the DataDirectory to the current working directory
    var dataDirectory = Directory.GetCurrentDirectory();
    AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    // Construct your SQLite connection string; adjust the file path as needed.
    string connectionString = $"Data Source=|DataDirectory|MyDatabase.db";
    optionsBuilder.UseSqlite(connectionString);

    return new AppDbContext(optionsBuilder.Options);
  }
}