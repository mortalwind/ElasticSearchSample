using ElasticSearchSample.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElasticSearchSample.API.Data;

public class ApplicationDbContext:DbContext
{
	public DbSet<User> users { get; set; }

    public ApplicationDbContext(DbContextOptions options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Lastname).IsRequired();
        });
    }

}
