
using System;
using System.Threading.Tasks;
using StartupIntrosBackend.Data;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Services;

public class NewsSourceService(DbService dbService)
{ 
  public async Task AddNewsSourceInteractiveAsync() 
  {
    Console.WriteLine("Select News Source Type:");
    Console.WriteLine("1: RSS Feed");
    Console.WriteLine("2: Twitter User");
    Console.WriteLine("3: Web Blog");
    Console.Write("Enter your choice (1, 2, or 3): ");
    var choice = Console.ReadLine();

    Console.Write("Enter News Source Name: ");
    var name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
      Console.WriteLine("Name must not be empty.");
      return;
    }

    switch (choice)
    {
      case "1":
        // RSS Feed: ask for URL
        Console.Write("Enter RSS Feed URL: ");
        var rssUrl = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(rssUrl))
        {
          Console.WriteLine("URL must not be empty.");
          return;
        }
        // Create an RSS Feed object.
        RssFeed rssFeed = new RssFeed(name, rssUrl);
        await dbService.AddRssFeedAsync(rssFeed);
        Console.WriteLine($"RSS Feed '{name}' added successfully.");
        break;

      case "2":
        // Twitter User: ask for Twitter handle.
        Console.Write("Enter Twitter Handle: ");
        var handle = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(handle))
        {
          Console.WriteLine("Handle must not be empty.");
          return;
        }
        // Create a TwitterUser object.
        TwitterUser twitterUser = new TwitterUser { Name = name, Handle = handle };
        await dbService.AddTwitterUserAsync(twitterUser);
        Console.WriteLine($"Twitter User '{name}' added successfully.");
        break;

      case "3":
        // Web Blog: ask for Blog URL.
        Console.Write("Enter Web Blog URL: ");
        var blogUrl = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(blogUrl))
        {
          Console.WriteLine("URL must not be empty.");
          return;
        }
        // Create a BlogPostSource object.
        BlogPostSource blogSource = new BlogPostSource { Name = name, Url = blogUrl };
        await dbService.AddBlogPostSourceAsync(blogSource);
        Console.WriteLine($"Web Blog '{name}' added successfully.");
        break;

      default:
        Console.WriteLine("Invalid choice. Operation aborted.");
        break;
    }
  }
}
