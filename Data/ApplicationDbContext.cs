using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNet.Models;

namespace ProjectNet.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

 public DbSet<BookingModel>? Bookings { get; set; }
 public DbSet<ServicesModel>? Services { get; set; }

 protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<BookingModel>()
        .HasOne(b => b.Service)
        .WithMany(s => s.Bookings)
        .HasForeignKey(b => b.ServiceId);
}


}
