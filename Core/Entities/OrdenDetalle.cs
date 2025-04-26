using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrdenDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Precio { get; set; }
        public string? Estado { get; set; }
        public int SubCategoriaId { get; set; }
        public int OrdenId { get; set; }
        [ForeignKey("SubCategoriaId")]
        public virtual SubCategoria? SubCategoria { get; set; }
        [ForeignKey("OrdenId")]
        public virtual Orden? Orden { get; set; }
        public virtual ICollection<OrdenDetalleHistorial>? OrdenDetalleHistorial { get; set; }
    }
}
