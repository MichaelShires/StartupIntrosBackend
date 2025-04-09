using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend.NewsSourceLib;

public class RssFeed : NewsSource
{
  [MaxLength(2048)]
  public string? Url { get; set; }

  public RssFeed(string name, string url) : base(name)
  {
    Url = url;
  }

  // Parameterless constructor for EF Core (if needed)
  public RssFeed() { }
}