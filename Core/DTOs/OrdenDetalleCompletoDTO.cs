using Core.Entities;
using System.Collections.ObjectModel;

namespace Core.DTOs
{
    public class OrdenDetalleCompletoDTO
    {
        public int OrdenDetalleId { get; set; }
        public int OrdenCabeceraId { get; set; }
        public string? OrdenDetalleNombre { get; set; }
        public string? Estado { get; set; }
        public int Monto { get; set; }
        public ObservableCollection<OrdenDetalleHistorial>? ListaOrdenDetallesHistorial { get; set; }
    }
}
