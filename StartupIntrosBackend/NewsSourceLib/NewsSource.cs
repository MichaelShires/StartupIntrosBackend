using System.ComponentModel.DataAnnotations;

namespace StartupIntrosBackend.NewsSourceLib;

public abstract class NewsSource
{
  public int Id { get; set; } // primary key
  [MaxLength(2048)]
  public string? Name { get; set; }
  public ICollection<Post> Posts { get; set; } = new List<Post>();
  
  // Protected constructor to ensure Name is set
  protected NewsSource(string name)
  {
    Name = name;
  }

  // Parameterless constructor for EF Core (if needed)
  protected NewsSource() { }
}