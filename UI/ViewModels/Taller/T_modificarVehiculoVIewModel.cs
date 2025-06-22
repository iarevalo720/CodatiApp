using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_modificarVehiculoVIewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public Vehiculo Vehiculo { get; set; } = new Vehiculo();
        public VehiculoDTO VehiculoDTO { get; set; } = new VehiculoDTO();
        public string TransmisionSelected { get; set; } = string.Empty;
        public string ServicioSelected { get; set; } = string.Empty;

        public MarcaVehiculo MarcaSelected { get; set; }
        public ModeloVehiculo ModeloSelected { get; set; }


        public List<string> ListaTransmision { get; set; }
        public List<ModeloVehiculo> Modelos { get; set; }
        public List<MarcaVehiculo> Marcas { get; set; }
        public T_modificarVehiculoVIewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;

            ListaTransmision = new List<string>
            {
                "AUTOMATICO",
                "MANUAL"
            };

            Marcas = new List<MarcaVehiculo> { };
            Modelos = new List<ModeloVehiculo> { };
        }

        public async Task ObtenervehiculoPorId(int id)
        {
            Vehiculo = await _vehiculoService.ObtenerVehiculoPorId(id);

            var marcaId = await _vehiculoService.ObtenerMarcaIdByModeloId(Vehiculo.ModeloVehiculoId);

            Marcas = (List<MarcaVehiculo>)await _vehiculoService.ObtenerMarcasHabilitadas();
            Modelos = (List<ModeloVehiculo>)await _vehiculoService.ObtenerModelosHabilitadosPorMarca(marcaId);

            MarcaSelected = Marcas.FirstOrDefault(m => m.Id == marcaId);
            ModeloSelected = Modelos.FirstOrDefault(m => m.Id == Vehiculo.ModeloVehiculoId);

            TransmisionSelected = Vehiculo.Transmision;
        }

        public async Task CargarModelosPorMarca()
        {
            ModeloSelected = new ModeloVehiculo();
            Modelos = (await _vehiculoService.ObtenerModelosHabilitadosPorMarca(MarcaSelected.Id)).ToList();
        }

        public async Task ModificarVehiculo()
        {
            if (!SonCamposValidos())
            {
                await Shell.Current.DisplayAlert("Informacion", "Por favor, complete todos campos primero", "OK");
                return;
            }

            try
            {
                Vehiculo.Transmision = TransmisionSelected;
                Vehiculo.ModeloVehiculoId = ModeloSelected.Id;
                Vehiculo.ModeloVehiculo = ModeloSelected;

                await _vehiculoService.ActualizarVehiculo(Vehiculo);

                await Shell.Current.DisplayAlert("Exito", "Se ha actualizado el vehículo exitosamente", "OK");
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo mas tarde", "OK");
            }
        }

        private bool SonCamposValidos()
        {
            if (string.IsNullOrWhiteSpace(Vehiculo.Matricula) ||
                string.IsNullOrWhiteSpace(Vehiculo.Anio) ||
                string.IsNullOrWhiteSpace(Vehiculo.Color) ||
                string.IsNullOrWhiteSpace(Vehiculo.Kilometraje) ||
                string.IsNullOrWhiteSpace(TransmisionSelected) ||
                MarcaSelected == null ||
                ModeloSelected == null)
            {
                return false;
            }
            return true;
        }
    }
}
