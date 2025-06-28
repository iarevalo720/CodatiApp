using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Timbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NumeroTimbrado { get; set; } = string.Empty;
        public string? FechaInicio { get; set; } = string.Empty;
        public string? FechaFin { get; set; } = string.Empty;
        public int PuntoEstablecimiento { get; set; }
        public int PuntoExpedicion { get; set; }
        public int NumeroSecuencialActual { get; set; }
        public int NumeroSecuencialMaximo { get; set; }
        public string? EsHabilitado { get; set; }
        public string? TimbradoActivo { get; set; }
    }
}
