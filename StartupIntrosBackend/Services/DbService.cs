using Microsoft.EntityFrameworkCore;
using StartupIntrosBackend.Data;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Services
{
  public class DbService
  {
    private readonly AppDbContext _context;

    private DbService(AppDbContext context)
    {
      _context = context;
    }

    public static async Task<DbService> CreateAsync()
    {
      var options = AppDbContext.CreateDbOptions();
      var context = new AppDbContext(options);
      await context.Database.MigrateAsync();
      await Task.WhenAll(
        context.Posts.ExecuteDeleteAsync(),
        context.NewsSources.ExecuteDeleteAsync()
      );
      return new DbService(context);
    }
    public async Task AddRssFeedAsync(RssFeed rssFeed)
    {
      _context.NewsSources.Add(rssFeed);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException { SqliteErrorCode: 19 })
        {
          Console.WriteLine($"Duplicate RSS URL found: {rssFeed.Url}. Skipping insert.");
        }
      }
    }
    
    public async Task AddTwitterUserAsync(TwitterUser twitterUser)
    {
      _context.NewsSources.Add(twitterUser);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException { SqliteErrorCode: 19 })
        {
          Console.WriteLine($"Duplicate Twitter Handle found: {twitterUser.Handle}. Skipping insert.");
        }
      }
    }
    
    public async Task AddBlogPostSourceAsync(BlogPostSource blogPostSource)
    {
      _context.NewsSources.Add(blogPostSource);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException { SqliteErrorCode: 19 })
        {
          Console.WriteLine($"Duplicate Web Blog URL found: {blogPostSource.Url}. Skipping insert.");
        }
      }
    }
    
    public async Task ProcessAllNewsFeedsAsync()
    {
      // Retrieve all news sources from the database asynchronously
      var sources = await _context.NewsSources.ToListAsync();
      NewsService service = new NewsService();
      foreach (var source in sources)
      {
        // Call the ReadRss method from RssReader which returns a list of posts for the given news source
        var posts = await service.GetPostsForNewsSourceAsync(source);
          
        // Add all posts to the Posts table
        _context.Posts.AddRange(posts);
          
        // Save changes to the database
        await _context.SaveChangesAsync();
      
        Console.WriteLine($"Processed {posts.Count} posts for source {source.Name}");
      }
    }
  }
}