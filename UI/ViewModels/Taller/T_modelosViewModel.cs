using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_modelosViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public IEnumerable<ModeloVehiculo> ListaModelos { get; set; } = Enumerable.Empty<ModeloVehiculo>();
        public string txtNombreModelo { get; set; } = string.Empty;
        public T_modelosViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }
        public async Task ObtenerModelosPorMarcaId(int marcaId)
        {
            ListaModelos = await _vehiculoService.ObtenerModelosPorMarca(marcaId);
            txtNombreModelo = string.Empty;
        }

        public async Task CambiarNombreModelo(int modeloId)
        {
            string respuesta = await Shell.Current.DisplayPromptAsync("Renombrar modelo", "Desea renombrar el modelo del vehiculo? Asigne su nuevo nombre", "Renombrar", "Cancelar");
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                ModeloVehiculo? modeloVehiculo = ListaModelos.FirstOrDefault(x => x.ModeloVehiculoId == modeloId);
                if (modeloVehiculo == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido obtener el modelo del vehiculo", "OK");
                    return;
                }
                modeloVehiculo.Nombre = respuesta.Trim();
                await GuardarModelo(modeloVehiculo);
            }
        }

        public async Task CambiarEstadoModelo(int modeloId)
        {
            ModeloVehiculo? modeloVehiculo = ListaModelos.FirstOrDefault(x => x.ModeloVehiculoId == modeloId);

            if (modeloVehiculo == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar el modelo del vehiculo", "OK");
                return;
            }

            modeloVehiculo.Habilitado = modeloVehiculo.Habilitado == "si" ? "no" : "si";

            await GuardarModelo(modeloVehiculo);
        }
        public async Task CrearNuevoModelo(int marcaId)
        {
            ModeloVehiculo nuevoModelo = ArmarModeloVehiculo(txtNombreModelo.Trim(), marcaId);
            try
            {
                await _vehiculoService.CrearModelo(nuevoModelo);
                await Shell.Current.DisplayAlert("Exito", "Modelo creado exitosamente", "OK");
                await ObtenerModelosPorMarcaId(marcaId);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido crear el modelo del vehiculo", "OK");
                throw;
            }
        }

        private async Task GuardarModelo(ModeloVehiculo modeloVehiculo)
        {
            try
            {
                await _vehiculoService.ActualizarModeloVehiculo(modeloVehiculo);
                await Shell.Current.DisplayAlert("Exito", "Se ha actualizado exitosamente", "OK");
                await ObtenerModelosPorMarcaId(modeloVehiculo.MarcaVehiculoId);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar el modelo del vehiculo", "OK");
                throw;
            }
        }
        private ModeloVehiculo ArmarModeloVehiculo(string nombreModelo, int marcaId)
        {
            return new ModeloVehiculo
            {
                Nombre = nombreModelo,
                MarcaVehiculoId = marcaId,
                Habilitado = "si"
            };
        }
    }
}
