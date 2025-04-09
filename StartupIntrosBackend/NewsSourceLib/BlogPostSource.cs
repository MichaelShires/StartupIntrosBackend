using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend.NewsSourceLib;

public class BlogPostSource : NewsSource
{
  [MaxLength(2048)]
  public required string Url { get; set; }
}