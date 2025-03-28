using Microsoft.EntityFrameworkCore;
using SCANX2.Models;  // Add this line to fix the "User not found" error

public class ScanXDbContext : DbContext
{
    public ScanXDbContext(DbContextOptions<ScanXDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
