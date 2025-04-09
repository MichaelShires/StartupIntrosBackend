
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
        .AddChoices(new[] { "RSS Feed", "Twitter User", "Blog"}));
    
    var name = AnsiConsole.Ask<string>("Enter [bold]News Source Name[/]: ");
    
    if (string.IsNullOrWhiteSpace(name))
    {
      Spectre.Console.AnsiConsole.MarkupLine("[red]Name must not be empty.[/]");
      return;
    }

    switch (sourceType)
    {
      case "RSS Feed":
        // RSS Feed: ask for URL
        var rssUrl = Spectre.Console.AnsiConsole.Ask<string>("Enter [bold]RSS Feed URL[/]:");
        if (string.IsNullOrWhiteSpace(rssUrl))
        {
          Spectre.Console.AnsiConsole.MarkupLine("[red]RSS Feed URL must not be empty.[/]");
          return;
        }
        // Create an RSS Feed object.
        RssFeed rssFeed = new RssFeed(name, rssUrl);
        await dbService.AddRssFeedAsync(rssFeed);
        Spectre.Console.AnsiConsole.MarkupLine($"[green]RSS Feed '{name}' added successfully.[/]");
        break;

      case "Twitter User":
        var handle = Spectre.Console.AnsiConsole.Ask<string>("Enter [bold]Twitter Handle[/]:");
        if (string.IsNullOrWhiteSpace(handle))
        {
          Spectre.Console.AnsiConsole.MarkupLine("[red]Twitter Handle must not be empty.[/]");
          return;
        }
        // Create a TwitterUser object.
        TwitterUser twitterUser = new TwitterUser { Name = name, Handle = handle };
        await dbService.AddTwitterUserAsync(twitterUser);
        Spectre.Console.AnsiConsole.MarkupLine($"[green]Twitter User '{name}' added successfully.[/]");
        break;

      case "Web Blog":
        var blogUrl = Spectre.Console.AnsiConsole.Ask<string>("Enter [bold]Web Blog URL[/]:");
        if (string.IsNullOrWhiteSpace(blogUrl))
        {
          Spectre.Console.AnsiConsole.MarkupLine("[red]Web Blog URL must not be empty.[/]");
          return;
        }
        // Create a BlogPostSource object.
        BlogPostSource blogSource = new BlogPostSource { Name = name, Url = blogUrl };
        await dbService.AddBlogPostSourceAsync(blogSource);
        Spectre.Console.AnsiConsole.MarkupLine($"[green]Web Blog '{name}' added successfully.[/]");
        break;

      default:
        Spectre.Console.AnsiConsole.MarkupLine("[red]Invalid choice. Operation aborted.[/]");
        break;
    }
  }
}
