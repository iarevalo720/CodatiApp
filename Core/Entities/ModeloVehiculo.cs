using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ModeloVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModeloVehiculoId { get; set; }
        public string? Nombre { get; set; }
        public int MarcaVehiculoId { get; set; }

        [ForeignKey("MarcaVehiculoId")]
        public virtual MarcaVehiculo? MarcaVehiculo { get; set; }
    }
}
