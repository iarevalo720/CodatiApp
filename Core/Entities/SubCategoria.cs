using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SubCategoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoriaId {get; set; }
        public string? Nombre { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
        public string? Habilitado { get; set; }
        [NotMapped]
        public string? TxtBtnCambiarEstadoServicio
        {
            get
            {
                return Habilitado == "si" ? "Inhabilitar" : "Habilitar";
            }
        }
    }
}
