using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Comprobante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FechaEmision { get; set; } = string.Empty;
        public string? Ruc { get; set; } = string.Empty;
        public string? NumeroComprobante { get; set; } = string.Empty;
        public int MontoTotal { get; set; }
        public int TimbradoId { get; set; }
        public int OrdenId { get; set; }
        public string? IdUsuario { get; set; }
        public virtual ApplicationUser? Usuario { get; set; }
        public virtual ComprobanteDetalle? ComprobanteDetalle { get; set; }

        [ForeignKey("OrdenId")]
        public virtual Orden? Orden { get; set; }

        [ForeignKey("TimbradoId")]
        public virtual Timbrado? Timbrado { get; set; }
    }
}
