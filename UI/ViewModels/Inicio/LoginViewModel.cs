using Core.DTOs;
using Core.Interfaces;
using System.Windows.Input;

namespace UI.ViewModels.Inicio
{
    public class LoginViewModel
    {
        private readonly IUserService _service;

        #region PROPIEDADES DE LA UI
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        #endregion

        #region COMANDOS
        public ICommand LoginCommand { get; }
        #endregion

        public LoginViewModel(IUserService service)
        {
            _service = service;
        }

        public async Task Login()
        {

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/contraseña vacia", "OK");
                return;
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
