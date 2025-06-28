using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ModeloVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int MarcaVehiculoId { get; set; }

        [ForeignKey("MarcaVehiculoId")]
        public virtual MarcaVehiculo? MarcaVehiculo { get; set; }
        public string? Habilitado { get; set; }
        [NotMapped]
        public string? TxtBtnCambiarEstadoModelo
        {
            get
            {
                return Habilitado == "si" ? "Inhabilitar" : "Habilitar";
            }
        }
    }
}
