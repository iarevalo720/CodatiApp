using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_funcionariosViewModel
    {
        private readonly IUserService _userService;
        public List<string> ListaRolesFuncionarios { get; set; }
        public ApplicationUser? User { get; set; } = new ApplicationUser();
        public string TxtCI { get; set; } = string.Empty;
        public string TxtNombre { get; set; } = string.Empty;
        public string TxtCorreo { get; set; } = string.Empty;
        public string TxtTelefono { get; set; } = string.Empty;
        public string TxtDireccion { get; set; } = string.Empty;
        public string TxtUsuarioHabilitado { get; set; } = string.Empty;
        public string TxtUsuarioActivadoPrimeraVez { get; set; } = string.Empty;
        public string RolFuncionarioSelected { get; set; } = string.Empty;
        public string RolActualFuncionario { get; set; } = string.Empty;

        public bool TxtCIEnabled { get; set; } = true;
        public bool TxtNombreEnabled { get; set; } = false;
        public bool TxtCorreoEnabled { get; set; } = false;
        public bool TxtTelefonoEnabled { get; set; } = false;
        public bool TxtDireccionEnabled { get; set; } = false;
        public string TxtBtnCambiarEstadoFuncionario { get; set; } = "INHABILITAR";

        public bool BtnBuscarEnabled { get; set; } = true;
        public bool BtnModificarEnabled { get; set; } = false;
        public bool BtnInhabilitarEnabled { get; set; } = false;
        public bool BtnCrearFuncionarioEnabled { get; set; } = true;
        public bool BtnCrearFuncionarioVisible { get; set; } = false;
        public bool BtnRestablecerContrasenaEnabled { get; set; } = false;
        public bool BtnRestablecerContrasenaVisible { get; set; } = false;

        public T_funcionariosViewModel(IUserService userService)
        {
            _userService = userService;

            ListaRolesFuncionarios = new List<string>
            {
                "Mecanico",
                "Secretaria"
            };

            BtnCrearFuncionarioVisible = DeviceInfo.Platform == DevicePlatform.WinUI;
            BtnRestablecerContrasenaVisible = DeviceInfo.Platform == DevicePlatform.WinUI;
        }

        public async Task ObtenerFuncionario()
        {
            if (string.IsNullOrWhiteSpace(TxtCI))
            {
                await Shell.Current.DisplayAlert("Información", "Agregue un número de cédula", "OK");
                return;
            }

            User = await _userService.ObtenerUsuarioPorCi(TxtCI);
            if (User is null)
            {
                await Shell.Current.DisplayAlert("Error", "No se encontró el usuario", "OK");
                return;
            }

            if (!await _userService.EsRolFuncionarioValido(User))
            {
                await Shell.Current.DisplayAlert("Error", "El usuario indicado no es un funcionario", "OK");
                return;
            }



            var rolActual = (await _userService.ObtenerRolesUsuario(User)).FirstOrDefault();

            RolFuncionarioSelected = rolActual;
            RolActualFuncionario = rolActual;

            TxtCI = User.NroDocumento;
            TxtNombre = User.Name;
            TxtCorreo = User.Email;
            TxtTelefono = User.PhoneNumber;
            TxtDireccion = User.Direccion;
            TxtUsuarioHabilitado = User.Habilitado;
            TxtUsuarioActivadoPrimeraVez = User.EsActivadoPrimeraVez;

            TxtCIEnabled = false;
            TxtNombreEnabled = true;
            TxtCorreoEnabled = true;
            TxtTelefonoEnabled = true;
            TxtDireccionEnabled = true;

            BtnBuscarEnabled = false;
            BtnModificarEnabled = true;
            BtnInhabilitarEnabled = true;
            BtnCrearFuncionarioEnabled = false;
            BtnRestablecerContrasenaEnabled = true;

            if (User.Habilitado?.ToLower() == "si")
            {
                TxtBtnCambiarEstadoFuncionario = "INHABILITAR";
            }
            else
            {
                TxtBtnCambiarEstadoFuncionario = "HABILITAR";
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
            RolFuncionarioSelected = string.Empty;
            TxtUsuarioActivadoPrimeraVez = string.Empty;

            TxtCIEnabled = true;
            TxtNombreEnabled = false;
            TxtCorreoEnabled = false;
            TxtTelefonoEnabled = false;
            TxtDireccionEnabled = false;

            BtnBuscarEnabled = true;
            BtnModificarEnabled = false;
            BtnInhabilitarEnabled = false;
            BtnCrearFuncionarioEnabled = true;
            BtnRestablecerContrasenaEnabled = false;
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

                if (RolActualFuncionario != RolFuncionarioSelected)
                {
                    await _userService.CambiarRolUsuario(User, RolActualFuncionario, RolFuncionarioSelected);
                }

                LimpiarCampos();

                await Shell.Current.DisplayAlert("Exito", "Datos actualiazados exitosamente", "OK");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                return;
            }
        }

        public async Task CambiarEstadoFuncionario()
        {
            bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "¿Está seguro de que desea cambiar el estado del funcionario?", "Sí", "No");
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
                await Shell.Current.DisplayAlert("Exito", "Estado del funcionario actualizado exitosamente", "OK");
                await ObtenerFuncionario();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                return;
            }
        }

        public async Task CrearFuncionario()
        {
            bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "¿Está seguro de que desea crear el funcionario?", "Sí", "No");
            if (!confirmar) return;

            bool esCamposValidos = validarCamposVacios();
            if (!esCamposValidos)
            {
                await Shell.Current.DisplayAlert("Informacion", "Primero complete todos los campos", "OK");
                return;
            }

            try
            {
                if (await _userService.ExisteEmail(TxtCorreo.Trim()))
                {
                    await Shell.Current.DisplayAlert("Informacion", "El correo ingresado ya existe, por favor, asigna un correo distinto", "OK");
                    return;
                }

                if (await _userService.ExisteCi(TxtCI.Trim()))
                {
                    await Shell.Current.DisplayAlert("Informacion", "La cédula ingresada ya existe, por favor, verifique a qué CI corresponde", "OK");
                    return;
                }

                await _userService.CrearFuncionario(TxtCI.Trim(), TxtNombre.Trim(), TxtCorreo.Trim(), TxtTelefono.Trim(), TxtDireccion.Trim(), RolFuncionarioSelected.Trim());

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
                if (ex.Message == "usuario_inexistente")
                {
                    await Shell.Current.DisplayAlert("Informacion", "No existe una cuenta con esa cedula", "OK");
                    return;
                }

                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                Console.WriteLine("Error al restablecer contraseña: " + ex.Message);
            }

            LimpiarCampos();
        }

        private bool validarCamposVacios()
        {
            if (string.IsNullOrWhiteSpace(TxtNombre) || string.IsNullOrWhiteSpace(TxtCorreo) || string.IsNullOrWhiteSpace(TxtTelefono) || string.IsNullOrWhiteSpace(TxtDireccion) || string.IsNullOrWhiteSpace(TxtCI))
            {
                return false;
            }

            return true;
        }
    }
}
