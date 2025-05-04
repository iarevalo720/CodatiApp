using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_crearComprobanteViewModel
    {
        public string FechaDeHoy { get; set; }
        public string OrdenId { get; set; }
        public string NombreCliente { get; set; }


        public T_crearComprobanteViewModel()
        {
            FechaDeHoy = DateTime.Now.ToString("dddd, dd 'de' MMMM 'del' yyyy", CultureInfo.CreateSpecificCulture("es-PY"));
        }

        public async Task ObtenerDatosComprobante(int nroOrden)
        {
            OrdenId = $"#{nroOrden}";

        }
    }
}
