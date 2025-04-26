using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Timbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string FechaInicio { get; set; } = string.Empty;
        public string FechaFin { get; set; } = string.Empty;
        public int PuntoEstablecimiento { get; set; }
        public string PuntoExpedicion { get; set; }
        public int NumeroSecuencial { get; set; }
    }
}
