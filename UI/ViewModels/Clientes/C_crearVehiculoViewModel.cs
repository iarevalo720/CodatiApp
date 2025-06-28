using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using System.Diagnostics;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_crearVehiculoViewModel
    {
        #region PROPIEDADES
        private readonly IVehiculoService _vehiculoService;
        public Vehiculo Vehiculo { get; set; } = new Vehiculo();
        public List<MarcaVehiculo> Marcas { get; set; } = new List<MarcaVehiculo>();
        public List<ModeloVehiculo> Modelos { get; set; } = new List<ModeloVehiculo>();
        public List<string> Transmision { get; set; } = new List<string>();
        public string CurrentTransmion { get; set; }
        public MarcaVehiculo SelectedMarca { get; set; }
        public ModeloVehiculo SelectedModelo { get; set; }
        #endregion


        public C_crearVehiculoViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;

            Transmision = new List<string> { "AUTOMATICO", "MANUAL" };
            CurrentTransmion = Transmision.FirstOrDefault();

            CargarMarcas();
        }


        public async Task CargarModelosPorMarca()
        {
            SelectedModelo = new ModeloVehiculo();
            Modelos = (await _vehiculoService.ObtenerModelosHabilitadosPorMarca(SelectedMarca.Id)).ToList();
        }

        private async void CargarMarcas()
        {
            Marcas = (await _vehiculoService.ObtenerMarcasHabilitadas()).ToList();
        }

        public async Task GuardarVehiculo()
        {
            try
            {
                if (!SonCamposValidos())
                {
                    await Shell.Current.DisplayAlert("Informacion", "Por favor, complete todos campos primero", "OK");
                    return;
                }

                string userId = await SecureStorage.GetAsync("id");
                Vehiculo.Transmision = CurrentTransmion;
                Vehiculo.ModeloVehiculo = SelectedModelo;
                Vehiculo.ModeloVehiculoId = SelectedModelo.Id;
                Vehiculo.Habilitado = "si";

                await _vehiculoService.CrearVehiculo(Vehiculo, userId);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo registrado exitosamente", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error, intentelo más tarde", "OK");
            };
        }

        private bool SonCamposValidos()
        {
            if (string.IsNullOrWhiteSpace(Vehiculo.Matricula) ||
                string.IsNullOrWhiteSpace(Vehiculo.Anio) ||
                string.IsNullOrWhiteSpace(Vehiculo.Color) ||
                string.IsNullOrWhiteSpace(Vehiculo.Kilometraje) ||
                string.IsNullOrWhiteSpace(CurrentTransmion) ||
                SelectedMarca == null ||
                SelectedModelo == null)
            {
                return false;
            }
            return true;
        }
    }
}
