using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_vehiculosViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IUserService _userService;
        public string TxtCI { get; set; } = string.Empty;
        public string txtNombreUsuario { get; set; } = string.Empty;
        public IEnumerable<VehiculoDTO?> ListaVehiculos { get; set; } = new List<VehiculoDTO>();

        public T_vehiculosViewModel(IVehiculoService vehiculoService, IUserService userService)
        {
            _vehiculoService = vehiculoService;
            _userService = userService;
        }
        public void LimpiarCampos()
        {
            TxtCI = string.Empty;
            txtNombreUsuario = string.Empty;
            ListaVehiculos = new List<VehiculoDTO>();
        }

        public async Task BtnBuscarVehiculo()
        {
            if (string.IsNullOrWhiteSpace(TxtCI))
            {
                await Shell.Current.DisplayAlert("Información", "Agregue un número de cédula", "OK");
                return;
            }

            ApplicationUser? user = await _userService.ObtenerUsuarioPorCi(TxtCI.Trim());

            if (user == null)
            {
                await Shell.Current.DisplayAlert("Información", "No se encontró el usuario", "OK");
                return;
            }

            try
            {
                ListaVehiculos = await _vehiculoService.ObtenerVehiculosDTO(user.Id);
                txtNombreUsuario = user.Name;
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo mas tarde", "OK");
                throw;
            }
        }
    }
}
