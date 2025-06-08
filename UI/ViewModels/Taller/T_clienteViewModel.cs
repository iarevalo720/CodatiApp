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
        public bool BtnCrearClienteEnabled { get; set; } = true;
        public bool BtnCrearClienteVisible { get; set; } = false;
        public bool BtnRestablecerContrasenaEnabled { get; set; } = false;
        public bool BtnRestablecerContrasenaVisible { get; set; } = false;

        public T_clienteViewModel(IUserService userService)
        {
            _userService = userService;
            BtnCrearClienteVisible = DeviceInfo.Platform == DevicePlatform.WinUI;
            BtnRestablecerContrasenaVisible = DeviceInfo.Platform == DevicePlatform.WinUI;
        }

        public async Task GuardarCambiosUsuario()
        {
            try
            {
                bool esCamposValidos = validarCamposVacios();

                if (!esCamposValidos)
                {
                    await Shell.Current.DisplayAlert("Informacion", "Primero complete todos los campos", "OK");
                    return;
                }

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
            BtnCrearClienteEnabled = false;
            BtnRestablecerContrasenaEnabled = true;

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
            BtnCrearClienteEnabled = true;
            BtnRestablecerContrasenaEnabled = false;
        }

        private bool validarCamposVacios()
        {
            if (string.IsNullOrWhiteSpace(TxtNombre) || string.IsNullOrWhiteSpace(TxtCorreo) || string.IsNullOrWhiteSpace(TxtTelefono) || string.IsNullOrWhiteSpace(TxtDireccion) || string.IsNullOrWhiteSpace(TxtCI))
            {
                return false;
            }

            return true;
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

        public async Task CrearCliente()
        {
            bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "¿Está seguro de que desea crear el cliente?", "Sí", "No");
            if (!confirmar) return;

            bool esCamposValidos = validarCamposVacios();
            if (!esCamposValidos)
            {
                await Shell.Current.DisplayAlert("Informacion", "Primero complete todos los campos", "OK");
                return;
            }

            try
            {
                if(await _userService.ExisteEmail(TxtCorreo.Trim()))
                {
                    await Shell.Current.DisplayAlert("Informacion", "El correo ingresado ya existe, por favor, asigna un correo distinto", "OK");
                    return;
                }

                if (await _userService.ExisteCi(TxtCI.Trim()))
                {
                    await Shell.Current.DisplayAlert("Informacion", "La cédula ingresada ya existe, por favor, verifique a qué CI corresponde", "OK");
                    return;
                }

                await _userService.CrearCliente(TxtCI.Trim(), TxtNombre.Trim(), TxtCorreo.Trim(), TxtTelefono.Trim(), TxtDireccion.Trim());

                await Shell.Current.DisplayAlert("Exito", "Cliente creado exitosamente", "OK");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                Console.WriteLine("Error al crear cliente: " + ex.Message);
            }
        }

        public async Task RestablecerContrasena()
        {
            bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "¿Está seguro de que desea restablecer la contraseña del usuario?", "Sí", "No");
            if (!confirmar) return;
            try
            {
                if (string.IsNullOrWhiteSpace(TxtCI))
                {
                    await Shell.Current.DisplayAlert("Informacion", "Primero debe buscar al usuario por su cédula", "OK");
                    return;
                }
                await _userService.RestablecerContrasena(TxtCI.Trim());
                await Shell.Current.DisplayAlert("Exito", "Contraseña restablecida exitosamente, se ha enviado un correo al usuario con la nueva contraseña", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                Console.WriteLine("Error al restablecer contraseña: " + ex.Message);
            }
        }
    }
}
