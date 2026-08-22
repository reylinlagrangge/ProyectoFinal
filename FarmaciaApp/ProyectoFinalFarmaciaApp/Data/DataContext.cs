using Microsoft.EntityFrameworkCore;
using ProyectoFinalFarmaciaApp.Data.Entities;
using System.Text.RegularExpressions;

namespace ProyectoFinalFarmaciaApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Batch> Batches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-S30456N\\SQL2026;Database=FarmaciaDB;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laboratory>().ToTable("Laboratory");
            modelBuilder.Entity<Medication>().ToTable("Medication");
            modelBuilder.Entity<Batch>().ToTable("Batch");
        }
    }
}