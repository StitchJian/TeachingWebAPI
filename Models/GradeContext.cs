using Microsoft.EntityFrameworkCore;

namespace grade_sheet_api.Models;

public class GradeContext : DbContext
{
    public GradeContext(DbContextOptions<GradeContext> options)
        : base(options)
    {
    }

    // 定義表
    public DbSet<Grade> Grades { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 也可在這定義表資訊
        modelBuilder.Entity<Grade>()
               .HasKey(m => new { m.number });
    }
}
