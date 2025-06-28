using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class CuentaBancaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreTitular { get; set; } = string.Empty;
        public string? NombreEntidad { get; set; } = string.Empty;
        public string? NroCuenta { get; set; } = string.Empty;
        public string? TipoDocumento { get; set; } = string.Empty;
        public string? NroDocumento { get; set; } = string.Empty;
    }
}
