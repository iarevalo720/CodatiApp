using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class MarcaVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Habilitado { get; set; }
        [NotMapped]
        public string? TxtBtnCambiarEstadoMarca 
        { 
            get
            {
                return Habilitado == "si" ? "Inhabilitar" : "Habilitar";
            }
        }
    }
}
