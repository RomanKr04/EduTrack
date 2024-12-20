using EduTrack.Models;
using Microsoft.EntityFrameworkCore;

public class UserDbContext : DbContext
{
    public DbSet<Student> Users { get; set; }
   

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}