
using Microsoft.EntityFrameworkCore;
using Vehicle.Models.Models;


namespace DataContext
{
    public class VehicleDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Vehicle.Models.Models.Vehicle> Vehicles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CencusTract> CencusTracts { get; set; }
        public DbSet<Electricity> Electricities { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-RDQ454BS\\SQLEXPRESS;Database='Electric Vehicles'; Trusted_Connection=True; TrustServerCertificate = True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vehicle.Models.Models.Vehicle>()
                .HasOne(e => e.Electricity)
                .WithMany(v => v.Vehicles)
                .HasForeignKey(e => e.ElectricityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle.Models.Models.Vehicle>()
                .HasOne(s => s.State)
                .WithMany(v => v.Vehicles)
                .HasForeignKey(s => s.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle.Models.Models.Vehicle>()
               .HasOne(s => s.City)
               .WithMany(v => v.Vehicles)
               .HasForeignKey(s => s.CityId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle.Models.Models.Vehicle>()
               .HasOne(s => s.CencusTract)
               .WithMany(v => v.Vehicles)
               .HasForeignKey(s => s.CencusTractId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle.Models.Models.Vehicle>()
               .HasOne(s => s.County)
               .WithMany(v => v.Vehicles)
               .HasForeignKey(s => s.CountyId)
               .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
