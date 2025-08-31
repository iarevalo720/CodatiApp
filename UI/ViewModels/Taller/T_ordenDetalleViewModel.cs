using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;
using QuestPDF.Fluent;
using UI.Utilities;
using UI.Views.Taller;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_ordenDetalleViewModel
    {
        private readonly IOrderService _ordenService;
        public OrdenCompletoDTO OrdenCompleto { get; set; } = new OrdenCompletoDTO();
        public string EstadoActual { get; set; } = string.Empty;
        public string EstadoInicial { get; set; } = string.Empty;
        public int TxtCostoTotal { get; set; }
        public bool BtnCrearComprobanteEnabled { get; set; }
        public bool BtnFinalizarOrdenEnabled { get; set; }
        public bool BtnCancelarOrdenEnabled { get; set; }
        public bool BtnCrearComprobanteVisible { get; set; }
        public bool btnCambiarEstadoOrdenCabeceraEnabled { get; set; }
        public bool btnIrGestionarOrdenDetalleEnabled { get; set; }
        public bool PickerEstadoOrdenCabecera { get; set; }
        public bool txtOrdenFinalizadoEnabled { get; set; }
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
            await RestringirAccesos();

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

            VerificarEstadoOrdenCabecera();
        }

        private void VerificarEstadoOrdenCabecera()
        {
            if (OrdenCompleto.EstadoOrden == "FINALIZADO")
            {
                BtnCrearComprobanteEnabled = true;
                BtnCancelarOrdenEnabled = false;
                txtOrdenFinalizadoEnabled = true;

                BtnFinalizarOrdenEnabled = false;
                PickerEstadoOrdenCabecera = false;
                btnCambiarEstadoOrdenCabeceraEnabled = false;
            }
            else if (OrdenCompleto.EstadoOrden == "CANCELADO" || OrdenCompleto.EstadoOrden == "RECHAZADO")
            {
                BtnCrearComprobanteEnabled = false;
                BtnCancelarOrdenEnabled = false;
                BtnFinalizarOrdenEnabled = false;
                txtOrdenFinalizadoEnabled = false;
            }
            else
            {
                BtnCrearComprobanteEnabled = false;
                BtnCancelarOrdenEnabled = true;
                BtnFinalizarOrdenEnabled = true;
                txtOrdenFinalizadoEnabled = false;
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
                await Application.Current.MainPage.DisplayAlert("Información", "La orden ya se encuentra finalizada", "OK");
                return;
            }

            bool confirmacion = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Está seguro de que desea finalizar la orden?", "Sí", "No");

            if (!confirmacion)
            {
                return; // Si el usuario cancela, no hacemos nada
            }

            try
            {
                await _ordenService.ActualizarEstadoOrdenCabecera(idUsuario, ordenId);
                
                await Application.Current.MainPage.DisplayAlert("Exito", "Orden finalizada exitosamente", "OK");
                await CargarOrdenCompletoAsync(ordenId);
            }
            catch (Exception ex)
            {
                if (ex.Message == "timbrado_vacio")
                {
                    await Shell.Current.DisplayAlert("Informacion", "No existe el timbrado seleccionado, por favor, asigne uno antes de finalizar la orden", "Ok");
                }
                else if (ex.Message == "timbrado_no_habilitado")
                {
                    await Shell.Current.DisplayAlert("Informacion", "El timbrado seleccionado se encuentra inhabilitado, por favor, cambie otro timbrado que se encuentre habilitado", "Ok");
                }
                else if (ex.Message == "timbrado_expirado")
                {
                    await Shell.Current.DisplayAlert("Informacion", "El timbrado ha vencido, por favor seleccione un timbrado válido o registre un nuevo timbrado legal", "Ok");
                }
                else if (ex.Message == "secuencial_maximo_alcanzado")
                {
                    await Shell.Current.DisplayAlert("Informacion", "El timbrado seleccionado ha alcanzado el número secuencial máximo, por favor registre un nuevo timbrado legal", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                }
                return;
            }
        }

        public async Task GenerarComprobante()
        {
            // 1. Crear el contenido del comprobante
            var comprobante = new ComprobantePdf
            {
                OrdenId = OrdenCompleto.OrdenId.ToString(),
                Cliente = OrdenCompleto.NombreUsuario,
                CI = OrdenCompleto.NroDocumento,
                Fecha = OrdenCompleto.FechaFinalizacionOrden,
                NumeroComprobante = OrdenCompleto.NumeroFactura,
                Timbrado = OrdenCompleto.NumeroTimbrado,
                Vencimiento = OrdenCompleto.FechaFinTimbrado,
                ListaOrdenDetalles = OrdenCompleto.ListaOrdenDetalleResumenes
                    .Select(t => (t.OrdenDetalleName, t.OrdenDetalleMonto))
                    .ToList()
            };

            // 2. Generar el PDF en memoria (byte[])
            var documento = comprobante.GeneratePdf();

            // 3. Guardarlo temporalmente en la carpeta de caché
            var tempFileName = $"comprobante_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var tempPath = Path.Combine(FileSystem.CacheDirectory, tempFileName);
            await File.WriteAllBytesAsync(tempPath, documento);

            // 4. Abrirlo con el visor predeterminado del sistema
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(tempPath)
            });
        }

        private async Task RestringirAccesos()
        {
            string rol = await SecureStorage.GetAsync("rol");

            if (rol != "Mecanico")
            {
                btnCambiarEstadoOrdenCabeceraEnabled = false;
                btnIrGestionarOrdenDetalleEnabled = false;
            }
            else
            {
                btnCambiarEstadoOrdenCabeceraEnabled = true;
                btnIrGestionarOrdenDetalleEnabled = true;
            }
        }
    }
}
