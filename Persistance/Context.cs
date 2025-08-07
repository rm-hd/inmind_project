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
        
        modelBuilder.Entity<Vehicle>((vehicle) =>
        {
            vehicle.HasKey(v => v.Id);
            vehicle.HasOne(v => v.Driver)
                .WithOne(d => d.Vehicle);
        });
        
        modelBuilder.Entity<Driver>((driver) =>
        {
            driver.HasKey(d => d.Id);
            driver.HasOne(d => d.Vehicle)
                .WithOne(v => v.Driver)
                .HasForeignKey<Driver>(d => d.VehicleId);
        });


        base.OnModelCreating(modelBuilder);

    }
}