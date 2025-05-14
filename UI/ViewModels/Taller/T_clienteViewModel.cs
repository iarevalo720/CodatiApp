using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{

    [AddINotifyPropertyChangedInterface]
    public class T_clienteViewModel
    {
        private readonly IUserService _userService;
        public ApplicationUser? User { get; set; }
        public string TxtCI { get; set; } = string.Empty;
        public string TxtNombre { get; set; } = string.Empty;
        public string TxtCorreo { get; set; } = string.Empty;
        public string TxtTelefono { get; set; } = string.Empty;
        public string TxtDireccion { get; set; } = string.Empty;
        public string TxtUsuarioHabilitado { get; set; } = string.Empty;

        public bool TxtCIEnabled { get; set; } = true;
        public bool TxtNombreEnabled { get; set; } = false;
        public bool TxtCorreoEnabled { get; set; } = false;
        public bool TxtTelefonoEnabled { get; set; } = false;
        public bool TxtDireccionEnabled { get; set; } = false;
        public string TxtBtnCambiarEstadoCliente { get; set; } = "INHABILITAR";

        public bool BtnBuscarEnabled { get; set; } = true;
        public bool BtnModificarEnabled { get; set; } = false;
        public bool BtnInhabilitarEnabled { get; set; } = false;

        public T_clienteViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task GuardarCambiosUsuario()
        {
            try
            {
                validarCamposVacios();

                User.Name = TxtNombre;
                User.Email = TxtCorreo;
                User.PhoneNumber = TxtTelefono;
                User.Direccion = TxtDireccion;

                await _userService.GuardarCambiosUsuario(User);
                await Shell.Current.DisplayAlert("Exito", "Datos actualiazados exitosamente", "OK");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                return;
            }
        }

        public async Task ObtenerUsuario(string ci)
        {
            if (string.IsNullOrWhiteSpace(ci))
            {
                await Shell.Current.DisplayAlert("Información", "Agregue un número de cédula", "OK");
                return;
            }

            User = await _userService.ObtenerUsuarioPorCi(ci);
            if (User is null)
            {
                await Shell.Current.DisplayAlert("Error", "No se encontró el usuario", "OK");
                return;
            }

            TxtCI = User.NroDocumento;
            TxtNombre = User.Name;
            TxtCorreo = User.Email;
            TxtTelefono = User.PhoneNumber;
            TxtDireccion = User.Direccion;
            TxtUsuarioHabilitado = User.Habilitado;

            TxtCIEnabled = false;
            TxtNombreEnabled = true;
            TxtCorreoEnabled = true;
            TxtTelefonoEnabled = true;
            TxtDireccionEnabled = true;

            BtnBuscarEnabled = false;
            BtnModificarEnabled = true;
            BtnInhabilitarEnabled = true;

            if (User.Habilitado?.ToLower() == "si")
            {
                TxtBtnCambiarEstadoCliente = "INHABILITAR";
            }
            else
            {
                TxtBtnCambiarEstadoCliente = "HABILITAR";
            }
        }

        public void LimpiarCampos()
        {
            TxtCI = string.Empty;
            TxtNombre = string.Empty;
            TxtCorreo = string.Empty;
            TxtTelefono = string.Empty;
            TxtDireccion = string.Empty;
            TxtUsuarioHabilitado = string.Empty;

            TxtCIEnabled = true;
            TxtNombreEnabled = false;
            TxtCorreoEnabled = false;
            TxtTelefonoEnabled = false;
            TxtDireccionEnabled = false;

            BtnBuscarEnabled = true;
            BtnModificarEnabled = false;
            BtnInhabilitarEnabled = false;
        }

        private async void validarCamposVacios()
        {

            if (string.IsNullOrWhiteSpace(TxtNombre) || string.IsNullOrWhiteSpace(TxtCorreo) || string.IsNullOrWhiteSpace(TxtTelefono) || string.IsNullOrWhiteSpace(TxtDireccion))
            {
                await Shell.Current.DisplayAlert("Información", "Complete todos los campos", "OK");
                return;
            }
        }

        public async Task CambiarEstadoCliente()
        {
            bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "¿Está seguro de que desea cambiar el estado del usuario?", "Sí", "No");

            if (!confirmar) return;

            try
            {
                if (User.Habilitado == "si")
                {
                    User.Habilitado = "no";
                }
                else
                {
                    User.Habilitado = "si";
                }
                await _userService.GuardarCambiosUsuario(User);
                await Shell.Current.DisplayAlert("Exito", "Estado del usuario actualizado exitosamente", "OK");
                await ObtenerUsuario(TxtCI);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                return;
            }
        }
    }
}
