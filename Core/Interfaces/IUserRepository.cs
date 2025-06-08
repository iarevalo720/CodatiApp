using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser?> ValidarEmail(string email);
        public Task<bool> ValidarPassword(ApplicationUser user, string password);
        public Task<IList<string>> ObtenerRoles(ApplicationUser user);
        public Task<ApplicationUser?> ObtenerUsuarioPorCI(string ci);
        public Task GuardarCambiosUsuario(ApplicationUser user);
        public Task<IdentityResult> CrearCliente(ApplicationUser applicationUser, string password);
        public Task AsignarRol(ApplicationUser user, string roleName);
        public Task<IdentityResult> ActualizarContrasena(ApplicationUser user, string contrasenaActual, string nuevaContrasena);
        public Task<IdentityResult> RestablecerContrasena(ApplicationUser user, string nuevaContrasena);
    }
}
