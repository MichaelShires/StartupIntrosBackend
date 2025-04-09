using Microsoft.EntityFrameworkCore;
using StartupIntrosBackend.Data;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Services;

public class NewsService
{
  // This method routes to the appropriate retrieval function based on the source subtype.
  public async Task<List<Post>> GetPostsForNewsSourceAsync(NewsSource source)
  {
    return source switch
    {
      RssFeed rss => await GetPostsForRssFeedAsync(rss),
      TwitterUser twitter => await GetPostsForTwitterAsync(twitter),
      BlogPostSource blog => await GetPostsForBlogPostSourceAsync(blog),
      _ => throw new NotSupportedException($"NewsSource of type {source.GetType().Name} is not supported.")
    };
  }
  
  
  private async Task<List<Post>> GetPostsForRssFeedAsync(RssFeed rss)
  {
    // Your existing RSS reader returns a list of posts.
    return await RssReaderService.ReadRss(rss);
  }

  private async Task<List<Post>> GetPostsForTwitterAsync(TwitterUser twitter)
  {
    // Implement your logic here to call the Twitter API and map the results to Post instances.
    // For example:
    // return await TwitterReader.ReadTweetsAsPostsAsync(twitter);
    throw new NotImplementedException("Twitter retrieval not implemented yet.");
  }

  private async Task<List<Post>> GetPostsForBlogPostSourceAsync(BlogPostSource blog)
  {
    // Implement your logic here to scrape the blog using Selenium and map the results to Post instances.
    // For example:
    // return await BlogScraper.ReadBlogPostsAsync(blog);
    throw new NotImplementedException("Blog retrieval not implemented yet.");
  }
}