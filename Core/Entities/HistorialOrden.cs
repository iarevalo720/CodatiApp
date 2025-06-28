using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class HistorialOrden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Fecha { get; set; } = string.Empty;
        public string? Hora { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public int OrdenId { get; set; }
        public string? IdUsuario { get; set; } = string.Empty;
    }
}
