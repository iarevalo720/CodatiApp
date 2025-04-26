using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ComprobanteDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Monto { get; set; }
        public int ComprobanteId { get; set; }
        public int OrdenDetalleId { get; set; }
        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? Comprobante { get; set; }
        [ForeignKey("OrdenDetalleId")]
        public virtual OrdenDetalle? OrdenDetalle { get; set; }
    }
}
