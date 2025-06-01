using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser?> ValidarEmail(string email);
        public Task<bool> ValidarPassword(ApplicationUser user, string password);
        public Task<IList<string>> ObtenerRoles(ApplicationUser user);
        public Task<ApplicationUser?> ObtenerUsuarioPorCI(string ci);
        public Task GuardarCambiosUsuario(ApplicationUser user);
    }
}
