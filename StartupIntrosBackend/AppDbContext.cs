using Microsoft.EntityFrameworkCore;

namespace StartupIntrosBackend;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<NewsSource> NewsSources { get; set; }
}