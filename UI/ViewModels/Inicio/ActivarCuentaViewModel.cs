using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Inicio
{
    [AddINotifyPropertyChangedInterface]
    public class ActivarCuentaViewModel
    {
        private readonly IUserService _userService;
        public string? CodigoActivacion { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public string? ConfirmarContrasena { get; set; }
        public ActivarCuentaViewModel(IUserService userService)
        {
            _userService = userService;
            
            CodigoActivacion = string.Empty;
            Correo = string.Empty;
            Contrasena = string.Empty;
            ConfirmarContrasena = string.Empty;
        }
        public async Task ActivarCuenta()
        {
            if (string.IsNullOrWhiteSpace(CodigoActivacion) || string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena) || string.IsNullOrWhiteSpace(ConfirmarContrasena))
            {
                await Shell.Current.DisplayAlert("Infornacion", "Debe completar todos los campos, son obligatorios", "OK");
                return;
            }

            if (Contrasena.Trim().Length < 6)
            {
                await Shell.Current.DisplayAlert("Infornacion", "La contraseña debe tener más de 5 caracteres", "OK");
                return;
            }

            if (Contrasena.Trim() != ConfirmarContrasena.Trim())
            {
                await Shell.Current.DisplayAlert("Infornacion", "Las contraseñas no coinciden", "OK");
                return;
            }

            try
            {
                bool existeCorreo = await _userService.ExisteEmail(Correo.Trim());
                if (!existeCorreo)
                {
                    await Shell.Current.DisplayAlert("Infornacion", "El correo electrónico no está registrado", "OK");
                    return;
                }

                await _userService.ActivarCuenta(CodigoActivacion.Trim(), Correo.Trim(), Contrasena.Trim());

                CodigoActivacion = string.Empty;
                Correo = string.Empty;
                Contrasena = string.Empty;
                ConfirmarContrasena = string.Empty;

                await Shell.Current.DisplayAlert("Exito", "Su cuenta se ha activado exitosamente, puede iniciar sesión", "OK");
            }
            catch (Exception ex)
            {
                if (ex.Message == "usuario_inexistente" || ex.Message == "codigo_invalido")
                {
                    await Shell.Current.DisplayAlert("Error", "Correo/contraseña invalida", "OK");
                }
                else if (ex.Message == "usuario_ya_activado")
                {
                    await Shell.Current.DisplayAlert("Informacion", "La cuenta ya se encuentra activada", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Ocurrió un error al activar la cuenta, intentelo más tarde ", "OK");
                }
            }
        }
    }
}
