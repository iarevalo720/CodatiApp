using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehiculoId { get; set; }
        public string? Matricula { get; set; }
        public string? Anio { get; set; }
        public string? Color { get; set; }
        public string? FechaAlta { get; set; }
        public int ModeloVehiculoId { get; set; }

        [ForeignKey("ModeloVehiculoId")]
        public virtual ModeloVehiculo? ModeloVehiculo { get; set; }
        public string? UserId { get; set; }
        public string? Kilometraje { get; set; }
        public string? Transmision { get; set; }
        public string? Habilitado { get; set; }
    }
}
