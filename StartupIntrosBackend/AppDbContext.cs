using Microsoft.EntityFrameworkCore;

namespace StartupIntrosBackend;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<NewsSource> NewsSources { get; set; }
  public DbSet<Post> Posts { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Post>()
      .HasOne(p => p.Author)
      .WithMany(ns => ns.Posts)  // Assuming you add a collection navigation property in NewsSource
      .HasForeignKey(p => p.AuthorId)
      .IsRequired();  // or .IsRequired(false) if you want it to be optional
  }
}