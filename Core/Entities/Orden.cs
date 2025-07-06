using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FechaCreacion { get; set; }
        public string? FechaFinalizacion { get; set; }
        public string? Estado { get; set; }
        public string? IdUsuario { get; set; }
        public int VehiculoId { get; set; }
        public string? ObservacionCliente { get; set; }
        public string? ComentarioRechazo { get; set; }
        public int MontoTotal { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroTimbrado { get; set; }
        public virtual ICollection<OrdenDetalle>? OrdenDetalles { get; set; }
        public virtual ICollection<HistorialOrden>? HistorialOrdenes { get; set; }
        public virtual ApplicationUser? Usuario { get; set; }
        [ForeignKey("VehiculoId")]
        public virtual Vehiculo? Vehiculo { get; set; }
        public int? IdTimbrado { get; set; }
        [ForeignKey("IdTimbrado")]
        public virtual Timbrado? Timbrado { get; set; }
    }
}
