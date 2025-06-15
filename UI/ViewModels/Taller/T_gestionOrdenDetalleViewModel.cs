using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_gestionOrdenDetalleViewModel
    {
        private readonly IOrderService _ordenService;
        public OrdenDetalleCompletoDTO OrdenDetalleCompleto { get; set; } = new OrdenDetalleCompletoDTO(); // Initialize to avoid null
        public string EstadoActual { get; set; } = string.Empty; // Initialize to avoid null
        public string EstadoInicial { get; set; } = string.Empty; // Initialize to avoid null
        public string TxtObservacion { get; set; } = string.Empty;
        public int TxtCosto { get; set; }
        public List<string> EstadosDisponibles => ListaEstadosOrdenDetalleDTO.ListaEstadosOrdenDetalles;

        public T_gestionOrdenDetalleViewModel(IOrderService orderService)
        {
            _ordenService = orderService;
        }

        public async Task CargarOrdenDetalleCompletoAsync(int ordenDetalleId)
        {
            try
            {
                await ActualizarOrdenDetalleCompleto(ordenDetalleId);
                EstadoActual = OrdenDetalleCompleto.Estado;
                EstadoInicial = EstadoActual;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task EnviarObservacion(int ordenDetalleId)
        {
            try
            {
                string nombreUsuario = await SecureStorage.GetAsync("nombre");
                string idUsuario = await SecureStorage.GetAsync("id");

                await _ordenService.GuardarObservacionOrdenDetalle(ordenDetalleId, TxtObservacion, EstadoActual, TxtCosto, nombreUsuario, idUsuario);

                await Application.Current.MainPage.DisplayAlert("Exito", "Observacion guardado exitosamente", "OK");
                await ActualizarOrdenDetalleCompleto(ordenDetalleId);
                TxtObservacion = string.Empty;
                TxtCosto = 0;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
            }
        }

        private async Task ActualizarOrdenDetalleCompleto(int ordenDetalleId)
        {
            OrdenDetalleCompleto = await _ordenService.ObtenerOrdenDetalleCompleto(ordenDetalleId);
        }
    }
}
