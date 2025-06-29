using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        public Task<string> GetCantidadOrdenesPendientes();
        public Task<IEnumerable<OrdenResumenDTO>> GetOrdenResumen();
        public Task AceptarOrden(int ordenId, string idUsuario);
        public Task RechazarOrden(int ordenId, string idUsuario, string comentarioRechazo);
        public Task<OrdenCompletoDTO> ObtenerOrdenCompleto(int idOrden);
        public Task<bool> ActualizarEstadoOrdenCabecera(string estado, string idUsuario, int ordenId);
        public Task GuardarObservacionOrdenDetalle(int ordenDetalleId, string observacion, string estadoActual, int costo, string nombreUsuario, string idUsuario);
        public Task<OrdenDetalleCompletoDTO> ObtenerOrdenDetalleCompleto(int ordenDetalleId);
        public Task CrearOrden(Orden orden);
        public Task CrearOrdenDetalle(int idOrden, List<int> listaSubCategoriaId);
        public Task GuardarTimbrado(string numeroTimbrado, string puntoEstablecimiento, string puntoExpedicion, string numeroSecuencialMaximo, DateTime fechaInicio, DateTime fechaFin);
        public Task<List<Timbrado>> ObtenerTimbrados();
        public Task ActualizarTimbrado(Timbrado timbrado);
    }
}
