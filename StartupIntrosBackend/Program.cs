using Microsoft.EntityFrameworkCore;
using StartupIntrosBackend.NewsSourceLib;

namespace StartupIntrosBackend;

public class Program
{
  public static async Task Main(string[] args)
  {
    var options = AppDbContext.CreateDbOptions();
    await using var context = new AppDbContext(options);
    await context.Database.MigrateAsync();
    
    await context.Posts.ExecuteDeleteAsync();
    await context.NewsSources.ExecuteDeleteAsync();
    RssFeed rssFeed = new RssFeed("techCrunch", "https://techcrunch.com/feed/");
    context.NewsSources.Add(rssFeed);
    await context.SaveChangesAsync();
      
    await ContentManager.ProcessAllNewsFeeds(context);
  }
}