using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrdenDetalleHistorial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public int OrdenDetalleId { get; set; }
        [ForeignKey("OrdenDetalleId")]
        public virtual OrdenDetalle? OrdenDetalle { get; set; }
    }
}
