using Microsoft.EntityFrameworkCore;
using SecureMindAPI.Models;

namespace SecureMindAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Incidents> CrimeIncidents { get; set; }
        public DbSet<Reports> AnonymousReports { get; set; }
        public DbSet<Counsellors> MentalHealthProfessionals { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incidents>()
            .HasOne(ci => ci.Location)
            .WithMany(l => l.CrimeIncidents)
            .HasForeignKey(ci => ci.LocationId);

            modelBuilder.Entity<Reports>()
            .HasOne(ar => ar.Location)
            .WithMany(l => l.AnonymousReports)
            .HasForeignKey(ar => ar.LocationId);

            modelBuilder.Entity<Counsellors>()
            .HasOne(mhp => mhp.Location)
            .WithMany(l => l.MentalHealthProfessionals)
            .HasForeignKey(mhp => mhp.LocationId);


        }
    }
}
