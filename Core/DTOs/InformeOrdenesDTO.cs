using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class InformeOrdenesDTO
    {
        public int TotalOrdenes { get; set; }
        public int OrdenesEnProceso { get; set; }
        public int OrdenesFinalizadas { get; set; }
        public int OrdenesRechazadas { get; set; }
        public List<ServicioFrecuenciaDTO> ServiciosMasSolicitados { get; set; } = new List<ServicioFrecuenciaDTO>();
    }

    public class ServicioFrecuenciaDTO
    {
        public string Servicio { get; set; } = string.Empty;
        public int Frecuencia { get; set; }
    }
}
