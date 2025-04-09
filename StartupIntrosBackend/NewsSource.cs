using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend;

public class NewsSource
{
  public int Id { get; set; } // primary key
  public required string Url { get; set; }
}