using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class MarcaVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarcaVehiculoId { get; set; }
        public string? Nombre { get; set; }
        public string? Habilitado { get; set; }
    }
}
