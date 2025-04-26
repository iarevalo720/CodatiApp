using Core.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_menuViewModel
    {
        private readonly IOrderService _orderService;
        public bool btnOrdenesVisible { get; set; }
        public bool btnClientesVisible { get; set; }
        public bool btnFuncionariosVisible { get; set; }
        public bool btnCategoriaVisible { get; set; }
        public bool btnSubcategoriaVisible { get; set; }
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
                case "mecanico":
                    btnOrdenesVisible = true;
                    btnClientesVisible = false;
                    btnFuncionariosVisible = false;
                    btnCategoriaVisible = false;
                    btnSubcategoriaVisible = false;
                    break;

                case "Secretaria":
                    btnOrdenesVisible = true;
                    btnClientesVisible = true;
                    btnFuncionariosVisible = false;
                    btnCategoriaVisible = false;
                    btnSubcategoriaVisible = false;
                    break;

                case "admin":
                    btnOrdenesVisible = true;
                    btnClientesVisible = true;
                    btnFuncionariosVisible = true;
                    btnCategoriaVisible = true;
                    btnSubcategoriaVisible = true;
                    break;
            }
        }

        public async Task GetCantidadOrdenesPendientes()
        {
            TxtOrdenesAVerificar = await _orderService.GetCantidadOrdenesPendientes();
        }
    }
}
