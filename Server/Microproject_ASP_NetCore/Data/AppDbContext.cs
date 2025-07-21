using Microsoft.EntityFrameworkCore;
using Microproject_ASP_NetCore.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Seat> Seats { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<ShowTime> ShowTimes { get; set; }
    public DbSet<Booking> Booking { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);
    }

}
