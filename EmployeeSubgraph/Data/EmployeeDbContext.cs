using Microsoft.EntityFrameworkCore;

namespace EmployeeSubgraph.Data;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

    public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EmployeeEntity>(b =>
        {
            b.ToTable("Employee");
            b.HasKey(e => e.EmployeeId);
            b.Property(e => e.FirstName).HasMaxLength(100);
            b.Property(e => e.LastName).HasMaxLength(100);
        });
    }
}

public class EmployeeEntity
{
    public int EmployeeId { get; set; }
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
