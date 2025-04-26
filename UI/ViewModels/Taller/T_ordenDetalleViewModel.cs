using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;
using UI.Views.Taller;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_ordenDetalleViewModel
    {
        private readonly IOrderService _ordenService;
        public OrdenCompletoDTO OrdenCompleto { get; set; } = new OrdenCompletoDTO(); // Initialize to avoid null
        public string EstadoActual { get; set; } = string.Empty; // Initialize to avoid null
        public string EstadoInicial { get; set; } = string.Empty; // Initialize to avoid null
        public int TxtCostoTotal { get; set; }
        public bool BtnCompletarOrdenEnabled { get; set; }
        public List<string> EstadoDisponibles => ListaEstadosOrdenDTO.ListaEstados;

        //COMANDOS
        public Command<int> IrGestionarOrdenDetalleCommand { get; }

        public T_ordenDetalleViewModel(IOrderService orderService)
        {
            _ordenService = orderService;
            IrGestionarOrdenDetalleCommand = new Command<int>((OrdenDetalleId) => GestionarOrdenDetalleCommand(OrdenDetalleId));
        }

        public async Task CargarOrdenCompletoAsync(int ordenId)
        {
            var listaOrdenCompleto = await _ordenService.ObtenerOrdenCompleto(ordenId);
            OrdenCompleto = listaOrdenCompleto;
            EstadoActual = OrdenCompleto.EstadoOrden;
            EstadoInicial = EstadoActual;
            TxtCostoTotal = 0;
            VerificarEstadosOrdenDetalle();

            foreach (var ordenDetalle in OrdenCompleto.ListaOrdenDetalleResumenes)
            {
                TxtCostoTotal += ordenDetalle.OrdenDetalleMonto;
            }
        }

        public async Task ActualizarOrdenCabecera(int ordenId)
        {
            var idUsuario = await SecureStorage.GetAsync("id");
            if (EstadoInicial != EstadoActual)
            {
                bool cambioExitoso = await _ordenService.ActualizarEstadoOrdenCabecera(EstadoActual, idUsuario, ordenId);
                if (cambioExitoso)
                {
                    await Application.Current.MainPage.DisplayAlert("Exito", "Estado actualizado exitosamente", "OK");
                    await CargarOrdenCompletoAsync(ordenId);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                }
            }
        }

        public async void GestionarOrdenDetalleCommand(int ordenDetalleId)
        {
            var ruta = $"{nameof(T_gestionOrdenDetalle)}?nroOrdenDetalle={ordenDetalleId}";
            await Shell.Current.GoToAsync(ruta);
        }

        private void VerificarEstadosOrdenDetalle()
        {
            foreach (var ordenDetalle in OrdenCompleto.ListaOrdenDetalleResumenes)
            {
                if (ordenDetalle.OrdenDetalleEstado != "TERMINADO")
                {
                    BtnCompletarOrdenEnabled = false;
                    break;
                }

                BtnCompletarOrdenEnabled = true;
            }
        }
    }
}
