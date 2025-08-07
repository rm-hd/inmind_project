using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistance;

public class Context: DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity relationships and properties here
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.Driver)
            .WithOne(d => d.Vehicle);
           
        
        modelBuilder.Entity<Driver>()
            .HasOne(d => d.Vehicle)
            .WithOne(v => v.Driver)
            .HasForeignKey<Driver>(d => d.VehicleId);
        
        base.OnModelCreating(modelBuilder);
    }
}