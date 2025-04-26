using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class CuentaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(1)]
        public string EsAlias { get; set; } = string.Empty;
        public int? AliasId { get; set; }
        public int? CuentaId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
