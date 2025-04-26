using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_ordenesViewModel
    {
        private readonly IOrderService _service;
        public ObservableCollection<OrdenResumenDTO>? OrdenResumen { get; set; }

        public T_ordenesViewModel(IOrderService service)
        {
            _service = service;
            ObtenerListaOrdenes();
        }

        public async Task ObtenerListaOrdenes()
        {
            var lista = await _service.GetOrdenResumen();
            OrdenResumen = new ObservableCollection<OrdenResumenDTO>(lista);
        }

        public async Task<bool> AceptarOrden(int ordenId)
        {
            try
            {
                var idUsuario = await SecureStorage.GetAsync("id");
                if (idUsuario == null) return false;

                await _service.AceptarOrden(ordenId, idUsuario);
                await ObtenerListaOrdenes();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RechazarOrden(int ordenId, string comentarioRechazo)
        {
            try
            {
                var idUsuario = await SecureStorage.GetAsync("id");
                if (idUsuario == null) return false;

                await _service.RechazarOrden(ordenId, idUsuario, comentarioRechazo);
                await ObtenerListaOrdenes();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
