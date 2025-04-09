using Microsoft.EntityFrameworkCore;

namespace StartupIntrosBackend;

public class Program
{
  public static async Task Main(string[] args)
  {
    var options = AppDbContext.CreateDbOptions();
    await using var context = new AppDbContext(options);
    await context.Database.MigrateAsync();

    //context.NewsSources.Add(new NewsSource{Url = "https://techcrunch.com/feed/"});
    //await context.SaveChangesAsync();
      
    //await ContentManager.ProcessRssFeeds(context);
  }
}