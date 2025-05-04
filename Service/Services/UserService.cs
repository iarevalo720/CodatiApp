using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserSession> Login(string username, string password)
        {
            var user = await _userRepository.ValidarEmail(username);
            if (user is null) throw new Exception("usuario_no_encontrado");

            bool esPasswordValido = await _userRepository.ValidarPassword(user, password);
            if (!esPasswordValido) throw new Exception("credenciales_invalidos");

            var rolesUsuario = await _userRepository.ObtenerRoles(user);
            var userSession = new UserSession(user.Id, user.Name, user.Email, rolesUsuario.First());

            try
            {
                return userSession;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser?> ObtenerUsuarioPorCi(string ci)
        {
            return await _userRepository.ObtenerUsuarioPorCI(ci);
        }
    }
}
