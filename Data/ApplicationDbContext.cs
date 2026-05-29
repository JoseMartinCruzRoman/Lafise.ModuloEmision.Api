using Microsoft.EntityFrameworkCore;
using Lafise.ModuloEmision.Api.Models.Entities;

namespace Lafise.ModuloEmision.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Poliza> Polizas { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<PolizaCobertura> PolizaCoberturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poliza>()
                .Property(p => p.PrimaTotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Poliza>()
                .Property(p => p.SumaAsegurada)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Vehiculo>()
                .Property(v => v.ValorComercial)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Poliza)
                .WithMany()
                .HasForeignKey(pc => pc.PolizaId);

            modelBuilder.Entity<PolizaCobertura>()
                .HasOne(pc => pc.Cobertura)
                .WithMany()
                .HasForeignKey(pc => pc.CoberturaId);
        }
    }
}