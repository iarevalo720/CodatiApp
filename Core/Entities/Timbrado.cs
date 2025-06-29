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
        public string? PuntoEstablecimiento { get; set; }
        public string? PuntoExpedicion { get; set; }
        public int NumeroSecuencialActual { get; set; }
        public int NumeroSecuencialMaximo { get; set; }
        public string? EsHabilitado { get; set; }
        public string? TimbradoSeleccionado { get; set; }
        [NotMapped]
        public string? TxtBtnCambiarEstadoTimbrado
        {
            get
            {
                return EsHabilitado == "si" ? "Inhabilitar" : "Habilitar";
            }
        }

        [NotMapped]
        public string? NumeroSecuencialMaximoFormateado
        {
            get
            {
                return (NumeroSecuencialMaximo.ToString()).PadLeft(7, '0');
            }
        }

        [NotMapped]
        public bool BtnSeleccionarTimbradoEnabled
        {
            get
            {
                return TimbradoSeleccionado == "si" ? false : true;
            }
        }

        [NotMapped]
        public string TxtBtnSeleccionarTimbrado
        {
            get
            {
                return TimbradoSeleccionado == "si" ? "Seleccionado" : "Seleccionar";
            }
        }
    }
}
