namespace Core.DTOs
{
    public class OrdenCompletoDTO
    {
        public int OrdenId { get; set; }
        public string? EstadoOrden { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NroDocumento { get; set; }
        public string? NumeroFactura { get; set; }
        public string? NumeroTimbrado { get; set; }
        public string? FechaFinTimbrado { get; set; }
        public string? FechaFinalizacionOrden { get; set; }
        public List<OrdenDetalleResumen>? ListaOrdenDetalleResumenes { get; set; }
        public List<HistorialOrdenDTO>? ListaHistorialOrden { get; set; }
    }
}
