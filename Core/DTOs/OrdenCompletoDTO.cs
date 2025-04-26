namespace Core.DTOs
{
    public class OrdenCompletoDTO
    {
        public int OrdenId { get; set; }
        public string EstadoOrden { get; set; }
        public List<OrdenDetalleResumen> ListaOrdenDetalleResumenes { get; set; }
        public List<HistorialOrdenDTO> ListaHistorialOrden { get; set; }
    }
}
