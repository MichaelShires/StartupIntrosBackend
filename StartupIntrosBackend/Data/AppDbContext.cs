using Microsoft.EntityFrameworkCore;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<NewsSource> NewsSources { get; set; }
  public DbSet<Post> Posts { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<NewsSource>()
      .HasDiscriminator<string>("SourceType")
      .HasValue<RssFeed>("RSS")
      .HasValue<TwitterUser>("Twitter")
      .HasValue<BlogPostSource>("Blog");
    
    modelBuilder.Entity<RssFeed>()
      .HasIndex(n => n.Url)
      .IsUnique();
    
    modelBuilder.Entity<TwitterUser>()
      .HasIndex(n => n.Handle)
      .IsUnique();
    
    modelBuilder.Entity<BlogPostSource>()
      .HasIndex(n => n.Url)
      .IsUnique();
    
    modelBuilder.Entity<Post>()
      .HasOne(p => p.NewsSource)
      .WithMany(ns => ns.Posts)
      .HasForeignKey(p => p.AuthorId)
      .IsRequired();  // or .IsRequired(false) if you want it to be optional
  }
  
  public static DbContextOptions<AppDbContext> CreateDbOptions(string connectionString = "Data Source=MyDatabase.db")
  {
    return new DbContextOptionsBuilder<AppDbContext>()
      .UseSqlite(connectionString)
      .Options;
  }
}