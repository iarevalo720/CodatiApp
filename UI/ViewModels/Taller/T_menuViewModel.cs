using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_menuViewModel
    {
        private readonly IOrderService _orderService;
        public bool btnOrdenesVisible { get; set; }
        public bool btnClientesVisible { get; set; }
        public bool btnVehiculosVisible { get; set; }
        public bool btnCategoriaVisible { get; set; }
        public bool btnMarcasVisible { get; set; }
        public bool btnFuncionariosVisible { get; set; }
        public string? TxtOrdenesAVerificar { get; set; }

        public T_menuViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            menuPorRol();
            GetCantidadOrdenesPendientes();
        }

        private async Task menuPorRol()
        {
            var rol = await SecureStorage.GetAsync("rol");
            switch (rol)
            {
                case "Mecanico":
                    btnOrdenesVisible = true;
                    btnClientesVisible = false;
                    btnVehiculosVisible = false;
                    btnFuncionariosVisible = false;
                    btnCategoriaVisible = false;
                    btnMarcasVisible = false;
                    break;

                case "Secretario":
                    btnOrdenesVisible = true;
                    btnClientesVisible = true;
                    btnVehiculosVisible = true;
                    btnFuncionariosVisible = true;
                    btnCategoriaVisible = true;
                    btnMarcasVisible = true;
                    break;

                case "admin":
                    btnOrdenesVisible = true;
                    btnClientesVisible = true;
                    btnVehiculosVisible = true;
                    btnFuncionariosVisible = true;
                    btnCategoriaVisible = true;
                    btnMarcasVisible = true;
                    break;
            }
        }

        public async Task GetCantidadOrdenesPendientes()
        {
            TxtOrdenesAVerificar = await _orderService.GetCantidadOrdenesPendientes();
        }
    }
}
