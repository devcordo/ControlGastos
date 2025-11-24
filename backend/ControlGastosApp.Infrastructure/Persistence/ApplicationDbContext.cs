using ControlGastosApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<TipoGasto> TipoGastos { get; set; } = null!;
        public DbSet<FondoMonetario> FondosMonetarios { get; set; } = null!;
        public DbSet<Presupuesto> Presupuestos { get; set; } = null!;
        public DbSet<GastoEncabezado> GastosEncabezado { get; set; } = null!;
        public DbSet<GastoDetalle> GastosDetalle { get; set; } = null!;
        public DbSet<Deposito> Depositos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TipoGasto>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Codigo).HasMaxLength(20).IsRequired();
                b.HasIndex(t => t.Codigo).IsUnique();
                b.Property(t => t.Nombre).HasMaxLength(200).IsRequired();
            });

            modelBuilder.Entity<FondoMonetario>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Nombre).HasMaxLength(200).IsRequired();
                b.Property(t => t.Saldo).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Presupuesto>(b =>
            {
                b.HasKey(t => t.Id);
                b.HasIndex(t => new {t.TipoGastoId, t.Anio, t.Mes}).IsUnique();
                b.Property(t => t.Monto).HasPrecision(18, 2);
            });

            modelBuilder.Entity<GastoEncabezado>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Total).HasPrecision(18,2);
                b.HasMany(t => t.Detalles).WithOne().HasForeignKey(d => d.GastoEncabezadoId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GastoDetalle>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Monto).HasPrecision(18,2);
            });

            modelBuilder.Entity<Deposito>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Monto).HasPrecision(18, 2);
            });
        }
    }
}
