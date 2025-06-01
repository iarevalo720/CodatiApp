using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Comprobante { get; set; } = string.Empty;
        public int CuentaId { get; set; }
        [ForeignKey("CuentaId")]
        public virtual CuentaCliente? CuentaCliente { get; set; }
    }
}
