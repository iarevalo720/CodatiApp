using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> GetCantidadOrdenesPendientes();
        public Task<IEnumerable<OrdenResumenDTO>> GetOrdenResumen();
        public Task<Orden> GetOrdenById(int ordenId);
        public Task GuardarOrden(Orden orden);
        public Task CrearOrden(Orden orden);
        public Task GuardarHistorialOrdenCabecera(HistorialOrden historial);
        public Task<OrdenCompletoDTO?> GetOrdenCompleto(int idOrden);
        public Task GuardarOrdenDetalle(OrdenDetalle ordenDetalle);
        public Task GuardarOrdenDetalleHistorial(OrdenDetalleHistorial ordenDetalleHistorial);
        public Task<OrdenDetalle?> GetOrdenDetalle(int ordenDetalleId);
        public Task<OrdenDetalleCompletoDTO> GetOrdenDetalleCompleto(int idOrdenDetalle);
        public Task CrearOrdenDetalle(OrdenDetalle ordenDetalle);
        public Task CrearTimbrado(Timbrado timbrado);
        public Task<List<Timbrado>> ObtenerTimbrados();
        public Task ActualizarTimbrado(Timbrado timbrado);
    }
}
