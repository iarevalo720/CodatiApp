using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using System.Diagnostics;
using System.Windows.Input;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_crearVehiculoViewModel
    {
        #region COMANDOS
        public ICommand GuardarVehiculoCommand { get; }
        public ICommand ObtenerModelosPorMarcaCommander { get; }
        #endregion

        #region PROPIEDADES
        public Vehiculo Vehiculo { get; set; } = new Vehiculo();
        public List<MarcaVehiculo> Marcas { get; set; } = new List<MarcaVehiculo>();
        public List<ModeloVehiculo> Modelos { get; set; } = new List<ModeloVehiculo>();
        public List<string> Transmision { get; set; } = new List<string>();
        public string CurrentTransmion { get; set; }
        public MarcaVehiculo SelectedMarca
        {
            get => _selectedMarca;
            set
            {
                if (_selectedMarca != value)
                {
                    _selectedMarca = value;
                    ObtenerModelosPorMarcaCommander.Execute(null);
                }
            }
        }
        #endregion

        private readonly IVehiculoService _vehiculoService;
        private MarcaVehiculo _selectedMarca;

        public C_crearVehiculoViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;

            Transmision = new List<string> { "AUTOMATICO", "MANUAL" };
            CurrentTransmion = Transmision.FirstOrDefault();

            GuardarVehiculoCommand = new Command(async () => await GuardarVehiculo());
            ObtenerModelosPorMarcaCommander = new Command(() => ObtenerModelosPorMarca());
            CargarMarcas();
        }

        public void ObtenerModelosPorMarca()
        {
            CargarModelosPorMarca(SelectedMarca.MarcaVehiculoId);
        }

        private async Task CargarModelosPorMarca(int? marcaId)
        {
            if (marcaId.HasValue) Modelos = (await _vehiculoService.ObtenerModelosPorMarca(marcaId.Value)).ToList();
        }

        private async void CargarMarcas()
        {
            Marcas = (await _vehiculoService.ObtenerMarcasHabilitadas()).ToList();
        }

        private async Task GuardarVehiculo()
        {
            try
            {
                string userId = await SecureStorage.GetAsync("id");
                Vehiculo.Transmision = CurrentTransmion;
                await _vehiculoService.CrearVehiculo(Vehiculo, userId);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo creado exitosamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error, intentelo más tarde", "OK");
            };
        }
    }
}
