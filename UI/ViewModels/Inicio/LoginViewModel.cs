using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Inicio
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        private readonly IUserService _service;

        #region PROPIEDADES DE LA UI
        public string? Correo { get; set; } = string.Empty;
        public string? Contrasena { get; set; } = string.Empty;
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

            Correo = string.Empty;
            Contrasena = string.Empty;
        }

        public async Task GuardarCredenciales(UserSession user)
        {
            await SecureStorage.SetAsync("id", user.Id);
            await SecureStorage.SetAsync("nombre", user.Name);
            await SecureStorage.SetAsync("rol", user.Role);
        }
    }
}
