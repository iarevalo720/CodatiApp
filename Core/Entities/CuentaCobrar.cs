using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class CuentaCobrar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string FechaPagado { get; set; } = string.Empty;
        public string TipoDePago { get; set; } = string.Empty;
        public int? TransferenciaId { get; set; }
        public int ComprobanteId { get; set; }
        [ForeignKey("ComprobanteId")]
        public virtual Comprobante? Comprobante { get; set; }
    }
}
