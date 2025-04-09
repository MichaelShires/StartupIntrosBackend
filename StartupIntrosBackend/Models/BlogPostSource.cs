using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend.Models;

public class BlogPostSource : NewsSource
{
  [MaxLength(2048)]
  public string? Url { get; set; }
}