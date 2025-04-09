using Microsoft.EntityFrameworkCore.Design;

namespace StartupIntrosBackend.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var options = AppDbContext.CreateDbOptions();
    return new AppDbContext(options);
  }
}