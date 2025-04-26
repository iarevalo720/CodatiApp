using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_crearOrdenViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IOrderService _orderService;
        public Orden Ordenes { get; set; }
        public IEnumerable<VehiculoDTO> VehiculosDTO { get; set; }
        public ListaVehiculosDTO SelectedVehiculo { get; set; }
        public IEnumerable<ListaVehiculosDTO> ListadoVehiculos { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public IEnumerable<SubCategoriaDTO> SubCategorias { get; set; }
        public string TxtObservacion { get; set; }
        public Categoria SelectedCategoria { get; set; }
        public SubCategoriaDTO SelectedSubCategoria { get; set; }
        public ObservableCollection<SubCategoriaDTO> ListaSubcategoriaAgregar { get; set; }

        public ICommand btnEnviarPedidoCommand { get; }
        public ICommand BtnEliminarSubCategoriaCommand { get; }

        public C_crearOrdenViewModel(IVehiculoService vehiculoService, IOrderService orderService)
        {
            _vehiculoService = vehiculoService;
            _orderService = orderService;

            ListadoVehiculos = new List<ListaVehiculosDTO>();
            ListaSubcategoriaAgregar = new ObservableCollection<SubCategoriaDTO>();
            SelectedSubCategoria = new SubCategoriaDTO();

            VehiculosDTO = new List<VehiculoDTO>();
            btnEnviarPedidoCommand = new Command(async () => await EnviarOrden());
        }

        public async Task CargarVehiculos()
        {
            string idUsuario = await SecureStorage.GetAsync("id");
            VehiculosDTO = await _vehiculoService.ObtenerVehiculosDTO(idUsuario);
            Categorias = await _vehiculoService.GetCategoria();
            ListadoVehiculos = ListarVehiculos();
        }

        public async Task CargarSubCategorias()
        {
            SubCategorias = await _vehiculoService.GetSubCategoria(SelectedCategoria.CategoriaId);
        }
        public List<ListaVehiculosDTO> ListarVehiculos()
        {
            var listadoVehiculos = new List<ListaVehiculosDTO>();

            foreach (var vehiculo in VehiculosDTO)
            {
                listadoVehiculos.Add(
                    new ListaVehiculosDTO
                    {
                        Id = vehiculo.VehiculoId.ToString(),
                        ContenidoVehiculo = $"{vehiculo.MarcaVehiculoNombre} {vehiculo.ModeloVehiculoNombre} | {vehiculo.Matricula.ToString()} | {vehiculo.Color}"                            
                    }
                );
            }
            return listadoVehiculos;
        }
        public async Task EnviarOrden()
        {
            try
            {
                if (SelectedCategoria == null || SelectedSubCategoria == null || ListaSubcategoriaAgregar.Count <= 0 || SelectedVehiculo == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Información", "Por favor, complete todos los campos primero", "OK");
                    return;
                }

                var orden = await ArmarOrden();
                await _orderService.CrearOrden(orden);

                List<int> subcategoriasAEnviar = new List<int>();

                foreach (var subcategoria in ListaSubcategoriaAgregar)
                {
                    subcategoriasAEnviar.Add(subcategoria.SubCategoriaId);
                }

                await _orderService.CrearOrdenDetalle(orden.Id, subcategoriasAEnviar);

                await Application.Current.MainPage.DisplayAlert("Éxito", "Orden solicitado exitosamente", "OK");
                LimpiarCampos();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error, intentelo más tarde", "OK");
            }
        }

        public async Task<Orden> ArmarOrden()
        {
            string idUsuario = await SecureStorage.GetAsync("id");

            return new Orden
            {
                FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy"),
                Estado = "A_VERIFICAR",
                IdUsuario = idUsuario,
                VehiculoId = int.Parse(SelectedVehiculo.Id),
                ObservacionCliente = TxtObservacion,
                ComentarioRechazo = "",
                MontoTotal = 0
            };
        }
        private void LimpiarCampos()
        {
            SelectedCategoria = new Categoria();
            SelectedSubCategoria = new SubCategoriaDTO();
            SelectedVehiculo = new ListaVehiculosDTO();
            TxtObservacion = string.Empty;
            ListaSubcategoriaAgregar = new ObservableCollection<SubCategoriaDTO> { };
        }

        public void CargarListaSubCategorias()
        {
            if (SelectedSubCategoria != null)
            {
                if (!ListaSubcategoriaAgregar.Contains(SelectedSubCategoria))
                {
                    ListaSubcategoriaAgregar.Add(SelectedSubCategoria);
                }
            }
        }

        public void EliminarSubcategoria(string subcategoria)
        {
            var elementoAEliminar = ListaSubcategoriaAgregar.FirstOrDefault(l => l.Nombre == subcategoria);

            ListaSubcategoriaAgregar.Remove(elementoAEliminar);
        }
    }
}
