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
        public bool BtnCrearComprobanteEnabled { get; set; }
        public bool BtnFinalizarOrdenEnabled { get; set; }
        public bool BtnCancelarOrdenEnabled { get; set; }
        public bool BtnCrearComprobanteVisible { get; set; }
        public bool btnCambiarEstadoOrdenCabeceraEnabled { get; set; }
        public List<string> EstadoDisponibles => ListaEstadosOrdenDTO.ListaEstados;

        //COMANDOS
        public Command<int> IrGestionarOrdenDetalleCommand { get; }

        public T_ordenDetalleViewModel(IOrderService orderService)
        {
            _ordenService = orderService;
            IrGestionarOrdenDetalleCommand = new Command<int>((OrdenDetalleId) => GestionarOrdenDetalleCommand(OrdenDetalleId));
            BtnCrearComprobanteVisible = DeviceInfo.Platform == DevicePlatform.WinUI;
        }

        public async Task CargarOrdenCompletoAsync(int ordenId)
        {
            var listaOrdenCompleto = await _ordenService.ObtenerOrdenCompleto(ordenId);
            OrdenCompleto = listaOrdenCompleto;
            EstadoActual = OrdenCompleto.EstadoOrden;
            EstadoInicial = EstadoActual;
            TxtCostoTotal = 0;
            VerificarEstadosOrdenDetalle();
            VerificarEstadoOrdenCabecera();

            foreach (var ordenDetalle in OrdenCompleto.ListaOrdenDetalleResumenes)
            {
                TxtCostoTotal += ordenDetalle.OrdenDetalleMonto;
            }
        }

        private void VerificarEstadoOrdenCabecera()
        {
            if (OrdenCompleto.EstadoOrden == "FINALIZADO")
            {
                BtnCrearComprobanteEnabled = true;
                BtnCancelarOrdenEnabled = false;
            }
            else if (OrdenCompleto.EstadoOrden == "CANCELADO" || OrdenCompleto.EstadoOrden == "RECHAZADO")
            {
                BtnCrearComprobanteEnabled = false;
                BtnCancelarOrdenEnabled = false;
            }
            else
            {
                BtnCrearComprobanteEnabled = false;
                BtnCancelarOrdenEnabled = true;
            }

            BtnFinalizarOrdenEnabled = HabilitarBtnFinalizarOrden();
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
                    BtnCrearComprobanteEnabled = false;
                    break;
                }

                BtnCrearComprobanteEnabled = true;
            }
        }

        public async Task FinalizarOrden(int ordenId)
        {
            var idUsuario = await SecureStorage.GetAsync("id");
            if (EstadoActual == "FINALIZADO")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La orden ya se encuentra finalizada", "OK");
            }
            else
            {
                bool cambioExitoso = await _ordenService.ActualizarEstadoOrdenCabecera("FINALIZADO", idUsuario, ordenId);
                if (cambioExitoso)
                {
                    await Application.Current.MainPage.DisplayAlert("Exito", "Orden finalizada exitosamente", "OK");
                    await CargarOrdenCompletoAsync(ordenId);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                }
            }
        }

        private bool HabilitarBtnFinalizarOrden()
        {
            foreach (OrdenDetalleResumen ordenDetalle in OrdenCompleto.ListaOrdenDetalleResumenes)
            {
                if (ordenDetalle.OrdenDetalleEstado != "TERMINADO")
                {
                    return false;
                }
            }

            return true;
        }
    }
}
