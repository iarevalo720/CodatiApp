using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DTOs
{
    public class VehiculoDTO
    {
        public int Id { get; set; }
        public string? Matricula { get; set; }
        public string? Anio { get; set; }
        public string? Color { get; set; }
        public string? FechaAlta { get; set; }
        public string? Kilometraje { get; set; }
        public string? Transmision { get; set; }
        public string? ModeloVehiculoNombre { get; set; }
        public string? MarcaVehiculoNombre { get; set; }
        public string? Habilitado { get; set; }
        [NotMapped]
        public string? TxtBtnCambiarEstadoVehiculo
        {
            get
            {
                return Habilitado == "si" ? "Inhabilitar" : "Habilitar";
            }
        }
    }
}
