
using System;
using System.Threading.Tasks;
using Spectre.Console;
using StartupIntrosBackend.Data;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Services;

public class NewsSourceService(DbService dbService)
{ 
  public async Task AddNewsSourceInteractiveAsync()
  {
    var sourceType = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
        .Title("[green]Select News Source Type[/]")
        .PageSize(10)
        .AddChoices("RSS Feed", "Twitter User", "Blog"));
    
    var name = AnsiConsole.Ask<string>("Enter [bold]News Source Name[/]: ");
    
    if (string.IsNullOrWhiteSpace(name))
    {
      AnsiConsole.MarkupLine("[red]Name must not be empty.[/]");
      return;
    }

    switch (sourceType)
    {
      case "RSS Feed":
        // RSS Feed: ask for URL
        var rssUrl = AnsiConsole.Ask<string>("Enter [bold]RSS Feed URL[/]:");
        if (string.IsNullOrWhiteSpace(rssUrl))
        {
          AnsiConsole.MarkupLine("[red]RSS Feed URL must not be empty.[/]");
          return;
        }
        // Create an RSS Feed object.
        var rssFeed = new RssFeed(name, rssUrl);
        await dbService.AddRssFeedAsync(rssFeed);
        AnsiConsole.MarkupLine($"[green]RSS Feed '{name}' added successfully.[/]");
        break;

      case "Twitter User":
        var handle = AnsiConsole.Ask<string>("Enter [bold]Twitter Handle[/]:");
        if (string.IsNullOrWhiteSpace(handle))
        {
          AnsiConsole.MarkupLine("[red]Twitter Handle must not be empty.[/]");
          return;
        }
        // Create a TwitterUser object.
        var twitterUser = new TwitterUser { Name = name, Handle = handle };
        await dbService.AddTwitterUserAsync(twitterUser);
        AnsiConsole.MarkupLine($"[green]Twitter User '{name}' added successfully.[/]");
        break;

      case "Web Blog":
        var blogUrl = AnsiConsole.Ask<string>("Enter [bold]Web Blog URL[/]:");
        if (string.IsNullOrWhiteSpace(blogUrl))
        {
          AnsiConsole.MarkupLine("[red]Web Blog URL must not be empty.[/]");
          return;
        }
        // Create a BlogPostSource object.
        var blogSource = new BlogPostSource { Name = name, Url = blogUrl };
        await dbService.AddBlogPostSourceAsync(blogSource);
        AnsiConsole.MarkupLine($"[green]Web Blog '{name}' added successfully.[/]");
        break;

      default:
        AnsiConsole.MarkupLine("[red]Invalid choice. Operation aborted.[/]");
        break;
    }
  }
}
