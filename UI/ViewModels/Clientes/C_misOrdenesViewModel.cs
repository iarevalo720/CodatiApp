using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_misOrdenesViewModel
    {
        private readonly IOrderService _orderService;
        public ObservableCollection<OrdenResumenDTO> ListaOrdenes { get; set; }
        public bool EstaCargando { get; set; }
        public bool HayOrdenes { get; set; }
        public bool NoHayOrdenes { get; set; }
        public ICommand VerDetalleOrdenCommand { get; }


        public C_misOrdenesViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            ListaOrdenes = new ObservableCollection<OrdenResumenDTO>();
            // Inicializar el comando
            VerDetalleOrdenCommand = new Command<int>(async (ordenId) => await VerDetalleOrden(ordenId));
        }

        public async Task CargarMisOrdenes()
        {
            try
            {
                ListaOrdenes.Clear();
                EstaCargando = true;

                var userId = await SecureStorage.GetAsync("id");
                if (userId == null) return;

                var ordenes = await _orderService.GetOrdenesPorUsuario(userId);

                if (ordenes != null)
                {
                    NoHayOrdenes = false;
                    HayOrdenes = true;

                    foreach (var orden in ordenes)
                    {
                        ListaOrdenes.Add(orden);
                    }
                }
                else
                {
                    NoHayOrdenes = true;
                    HayOrdenes = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudieron cargar las órdenes", "OK");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        public async Task VerDetalleOrden(int ordenId)
        {
            // Aquí puedes implementar la navegación al detalle de la orden
            await Shell.Current.DisplayAlert("Información", $"Ver detalle de orden #{ordenId}", "OK");
        }
    }
}