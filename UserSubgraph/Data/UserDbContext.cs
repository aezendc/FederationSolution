using Microsoft.EntityFrameworkCore;

namespace UserSubgraph.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity>(b =>
        {
            b.ToTable("Users");
            b.HasKey(u => u.UserId);
            b.Property(u => u.Username).IsRequired().HasMaxLength(100);
            // Optional seed if database not yet initialized.
            //b.HasData(
            //    new UserEntity { UserId = 1, Username = "alice" },
            //    new UserEntity { UserId = 2, Username = "bob" }
            //);
        });
    }
}

public class UserEntity
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}