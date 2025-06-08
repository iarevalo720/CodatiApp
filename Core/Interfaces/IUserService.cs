using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserService
    {
        public Task<UserSession> Login(string username, string password);
        public Task<ApplicationUser?> ObtenerUsuarioPorCi(string ci);
        public Task<bool> ExisteEmail(string email);
        public Task<bool> ExisteCi(string ci);
        public Task GuardarCambiosUsuario(ApplicationUser user);
        public Task CrearCliente(string ci, string nombre, string correo, string telefono, string direccion);
        public Task ActivarCuenta(string codigoActivacion, string correo, string contrasena);
        public Task RestablecerContrasena(string ci);
    }
}
