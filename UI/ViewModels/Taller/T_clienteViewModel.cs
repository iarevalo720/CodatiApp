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
        public bool TxtUsuarioHabilitadoEnabled { get; set; } = false;

        public bool BtnBuscarEnabled { get; set; } = true;
        public bool BtnModificarEnabled { get; set; } = false;
        public bool BtnInhabilitarEnabled { get; set; } = false;

        public T_clienteViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public async void ObtenerUsuario(string ci)
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
            TxtUsuarioHabilitadoEnabled = true;


            BtnBuscarEnabled = false;
            BtnModificarEnabled = true;
            BtnInhabilitarEnabled = true;
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
            TxtUsuarioHabilitadoEnabled = false;

            BtnBuscarEnabled = true;
            BtnModificarEnabled = false;
            BtnInhabilitarEnabled = false;
        }
    }
}
