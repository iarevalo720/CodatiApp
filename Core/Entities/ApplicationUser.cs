using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? EsHabilitado { get; set; }
        public string? EsActivadoPrimeraVez { get; set; }
        public string? NroDocumento { get; set; }
        public string? Direccion { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
    }
}
