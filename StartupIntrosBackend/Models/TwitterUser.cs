using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend.Models;

public class TwitterUser : NewsSource
{
  [MaxLength(2048)]
  public required string Handle { get; set; }
}