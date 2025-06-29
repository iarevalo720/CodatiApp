using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<string> GetCantidadOrdenesPendientes()
        {
            int ordenesAVerificar = await _orderRepository.GetCantidadOrdenesPendientes();
            return ordenesAVerificar.ToString();
        }

        public async Task<IEnumerable<OrdenResumenDTO>> GetOrdenResumen()
        {
            return await _orderRepository.GetOrdenResumen();
        }

        public async Task AceptarOrden(int ordenId, string idUsuario)
        {
            var orden = await _orderRepository.GetOrdenById(ordenId);
            ValidarEstadoOrden(orden);
            orden.Estado = "ESPERANDO_VEHICULO";
            await _orderRepository.GuardarOrden(orden);
            string mensajeAceptado = "ORDEN ACEPTADA, EN ESPERA DEL VEHICULO DEL CLIENTE";
            HistorialOrden historial = ArmarHistorialOrden(orden, idUsuario, mensajeAceptado);
            await _orderRepository.GuardarHistorialOrdenCabecera(historial);
        }

        public async Task RechazarOrden(int ordenId, string idUsuario, string comentarioRechazo)
        {
            var orden = await _orderRepository.GetOrdenById(ordenId);
            ValidarEstadoOrden(orden);
            orden.Estado = "RECHAZADO";
            orden.ComentarioRechazo = comentarioRechazo;
            await _orderRepository.GuardarOrden(orden);
            string mensajeOrdenRechazada = "ORDEN RECHAZADA";
            HistorialOrden historial = ArmarHistorialOrden(orden, idUsuario, mensajeOrdenRechazada);
            await _orderRepository.GuardarHistorialOrdenCabecera(historial);
        }

        private void ValidarEstadoOrden(Orden orden)
        {
            if (orden.Estado != "A_VERIFICAR")
            {
                throw new Exception("La orden ya esta aceptada o rechazada");
            }
        }

        private HistorialOrden ArmarHistorialOrden(Orden orden, string idUsuario, string mensaje)
        {
            return new HistorialOrden
            {
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                Hora = DateTime.Now.ToString("HH:mm:ss"),
                IdUsuario = idUsuario,
                OrdenId = orden.Id,
                Descripcion = mensaje
            };
        }

        public async Task<OrdenCompletoDTO> ObtenerOrdenCompleto(int idOrden)
        {
            return await _orderRepository.GetOrdenCompleto(idOrden);
        }

        public async Task<bool> ActualizarEstadoOrdenCabecera(string estado, string idUsuario, int ordenId)
        {
            try
            {
                Orden orden = await _orderRepository.GetOrdenById(ordenId);
                orden.Estado = estado;
                await _orderRepository.GuardarOrden(orden);

                HistorialOrden historialOrden = ArmarHistorialOrden(orden, idUsuario, estado);
                await _orderRepository.GuardarHistorialOrdenCabecera(historialOrden);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task GuardarObservacionOrdenDetalle(int ordenDetalleId, string observacion, string estadoActual, int costo, string nombreUsuario, string idUsuario)
        {
            OrdenDetalle? ordenDetalle = await _orderRepository.GetOrdenDetalle(ordenDetalleId);
            if (ordenDetalle == null) throw new Exception("No se encontró el detalle de la orden");


            if (ordenDetalle.Estado != estadoActual)
            {
                ordenDetalle.Estado = estadoActual;
                await _orderRepository.GuardarOrdenDetalle(ordenDetalle);

                OrdenDetalleHistorial ordenDetalleHistorial = new OrdenDetalleHistorial
                {
                    Descripcion = estadoActual,
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    Hora = DateTime.Now.ToString("HH:mm"),
                    OrdenDetalleId = ordenDetalleId,
                    NombreUsuario = nombreUsuario,
                    IdUsuario = idUsuario
                };
                await _orderRepository.GuardarOrdenDetalleHistorial(ordenDetalleHistorial);
            }

            if (costo > 0 && ordenDetalle.Precio != costo)
            {
                ordenDetalle.Precio = costo;
                await _orderRepository.GuardarOrdenDetalle(ordenDetalle);

                OrdenDetalleHistorial ordenDetalleHistorial = new OrdenDetalleHistorial
                {
                    Descripcion = $"Costo del servicio: {costo}",
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    Hora = DateTime.Now.ToString("HH:mm"),
                    OrdenDetalleId = ordenDetalleId,
                    NombreUsuario = nombreUsuario,
                    IdUsuario = idUsuario
                };
                await _orderRepository.GuardarOrdenDetalleHistorial(ordenDetalleHistorial);
            }

            if (!string.IsNullOrWhiteSpace(observacion))
            {
                OrdenDetalleHistorial ordenDetalleHistorial = new OrdenDetalleHistorial
                {
                    Descripcion = observacion,
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                    Hora = DateTime.Now.ToString("HH:mm"),
                    OrdenDetalleId = ordenDetalleId,
                    NombreUsuario = nombreUsuario,
                    IdUsuario = idUsuario
                };
                await _orderRepository.GuardarOrdenDetalleHistorial(ordenDetalleHistorial);
            }
        }

        public async Task<OrdenDetalleCompletoDTO> ObtenerOrdenDetalleCompleto(int ordenDetalleId)
        {
            return await _orderRepository.GetOrdenDetalleCompleto(ordenDetalleId);
        }

        public async Task CrearOrden(Orden orden)
        {
            await _orderRepository.CrearOrden(orden);
        }

        public async Task CrearOrdenDetalle(int idOrden, List<int> listaSubCategoriaId)
        {
            foreach (int subCategoriaId in listaSubCategoriaId)
            {
                var ordenDetalle = ArmarOrdenDetalle(idOrden, subCategoriaId);
                await _orderRepository.CrearOrdenDetalle(ordenDetalle);
            }
        }

        private OrdenDetalle ArmarOrdenDetalle(int idOrden, int subCategoriaId)
        {
            return new OrdenDetalle()
            {
                Precio = 0,
                Estado = "INACTIVO",
                SubCategoriaId = subCategoriaId,
                OrdenId = idOrden
            };
        }

        public async Task GuardarTimbrado(string numeroTimbrado, string puntoEstablecimiento, string puntoExpedicion, string numeroSecuencialMaximo, DateTime fechaInicio, DateTime fechaFin)
        {
            Timbrado nuevoTimbrado = ArmarTimbrado(numeroTimbrado, puntoEstablecimiento, puntoExpedicion, numeroSecuencialMaximo, fechaInicio, fechaFin);
            await _orderRepository.CrearTimbrado(nuevoTimbrado);
        }

        private Timbrado ArmarTimbrado(string numeroTimbrado, string puntoEstablecimiento, string puntoExpedicion, string numeroSecuencialMaximo, DateTime fechaInicio, DateTime fechaFin)
        {
            return new Timbrado()
            {
                NumeroTimbrado = numeroTimbrado,
                PuntoEstablecimiento = puntoEstablecimiento,
                PuntoExpedicion = puntoExpedicion,
                NumeroSecuencialMaximo = int.Parse(numeroSecuencialMaximo),
                FechaInicio = fechaInicio.ToString("dd/MM/yyyy"),
                FechaFin = fechaFin.ToString("dd/MM/yyyy"),
                NumeroSecuencialActual = 0,
                TimbradoSeleccionado = "no",
                EsHabilitado = "si"
            };
        }

        public async Task<List<Timbrado>> ObtenerTimbrados()
        {
            return await _orderRepository.ObtenerTimbrados();
        }

        public async Task ActualizarTimbrado(Timbrado timbrado)
        {
            await _orderRepository.ActualizarTimbrado(timbrado);
        }
    }
}
