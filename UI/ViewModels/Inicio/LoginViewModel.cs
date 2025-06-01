using Core.DTOs;
using Core.Interfaces;

namespace UI.ViewModels.Inicio
{
    public class LoginViewModel
    {
        private readonly IUserService _service;

        #region PROPIEDADES DE LA UI
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        #endregion

        public LoginViewModel(IUserService service)
        {
            _service = service;
        }

        public async Task Login()
        {

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                throw new Exception("campos_vacios");
            }

            UserSession user = await _service.Login(Correo, Contrasena);
            await GuardarCredenciales(user);
        }

        public async Task GuardarCredenciales(UserSession user)
        {
            await SecureStorage.SetAsync("id", user.Id);
            await SecureStorage.SetAsync("nombre", user.Name);
            await SecureStorage.SetAsync("rol", user.Role);
        }
    }
}
