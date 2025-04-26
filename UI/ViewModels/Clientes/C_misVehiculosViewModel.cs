using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_misVehiculosViewModel
    {
        private readonly IVehiculoService _service;
        public IEnumerable<VehiculoDTO> Vehiculos { get; set; } = new List<VehiculoDTO>();

        public C_misVehiculosViewModel(IVehiculoService service)
        {
            _service = service;
        }

        public async Task ObtenerVehiculos()
        {
            string? userId = await SecureStorage.GetAsync("id");

            if (string.IsNullOrEmpty(userId))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha encontrado el ID del usuario", "OK");
                return;
            }

            IEnumerable<VehiculoDTO> lista = await _service.ObtenerVehiculosDTO(userId);
            Vehiculos = lista;
        }
    }
}
