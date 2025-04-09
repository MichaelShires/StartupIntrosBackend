using Microsoft.EntityFrameworkCore;

namespace StartupIntrosBackend;

public class ContentManager
{
  public static async Task ProcessAllNewsFeeds(AppDbContext context)
  {
    // Retrieve all news sources from the database asynchronously
    var sources = await context.NewsSources.ToListAsync();
    NewsService service = new NewsService();
    foreach (var source in sources)
    {
      // Call the ReadRss method from RssReader which returns a list of posts for the given news source
      var posts = await service.GetPostsForNewsSourceAsync(source);
          
      // Add all posts to the Posts table
      context.Posts.AddRange(posts);
          
      // Save changes to the database
      await context.SaveChangesAsync();
      
      Console.WriteLine($"Processed {posts.Count} posts for source {source.Name}");
    }
  }
}