using Core.Interfaces;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
            string jwt  = string.Empty;

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/contraseña vacia", "OK");
                return;
            }

            try
            {
                jwt = await _service.Login(Correo, Contrasena);
                await GuardarCredenciales(jwt);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                return;
            }

            return;
        }

        public async Task GuardarCredenciales(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;

            string userId = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString();
            string name = jsonToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value.ToString();
            string role = jsonToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.ToString();

            await SecureStorage.SetAsync("jwt", jwt);
            await SecureStorage.SetAsync("id", userId);
            await SecureStorage.SetAsync("nombre", name);
            await SecureStorage.SetAsync("rol", role);
        }
    }
}
