using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ModeloVehiculo> ModeloVehiculos { get; set; }
        public DbSet<MarcaVehiculo> MarcaVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<SubCategoria> SubCategoria { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }
        public DbSet<OrdenDetalleHistorial> OrdenDetalleHistoriales { get; set; }
        public DbSet<Timbrado> Timbrados { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<ComprobanteDetalle> ComprobanteDetalles { get; set; }
        public DbSet<HistorialOrden> HistorialOrdenes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Orden>()
                .HasOne(o => o.Usuario)
                .WithMany()
                .HasForeignKey(o => o.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
