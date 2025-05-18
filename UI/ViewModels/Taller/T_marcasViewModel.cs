using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_marcasViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public IEnumerable<MarcaVehiculo> ListaMarcas { get; set; } = new List<MarcaVehiculo>();
        public string? txtNombreMarca { get; set; } = string.Empty;

        public T_marcasViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task ObtenerTodasMarcas()
        {
            ListaMarcas = await _vehiculoService.ObtenerMarcas();
            txtNombreMarca = string.Empty;
        }

        public async Task CambiarNombreMarca(int marcaId)
        {
            string respuesta = await Shell.Current.DisplayPromptAsync("Renombrar marca", "Desea renombrar la marca del vehiculo? Asigne su nuevo nombre", "Renombrar", "Cancelar");
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                MarcaVehiculo? marcaVehiculo = ListaMarcas.FirstOrDefault(m => m.MarcaVehiculoId == marcaId);

                if (marcaVehiculo == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido obtener la marca del vehiculo", "OK");
                    return;
                }

                marcaVehiculo.Nombre = respuesta;
                try
                {
                    await _vehiculoService.ActualizarMarcaVehiculo(marcaVehiculo);
                    await Shell.Current.DisplayAlert("Exito", "Se ha actualizado exitosamente", "OK");
                    await ObtenerTodasMarcas();
                }
                catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar la marca del vehiculo", "OK");
                    throw;
                }
            }
        }

        public async Task CambiarEstadoMarca(int marcaId)
        {
            try
            {
                MarcaVehiculo? marcaVehiculo = ListaMarcas.FirstOrDefault(c => c.MarcaVehiculoId == marcaId);
                if (marcaVehiculo == null) throw new Exception("marca del vehiculo no encontrado");
                marcaVehiculo.Habilitado = marcaVehiculo.Habilitado == "si" ? "no" : "si";
                await _vehiculoService.ActualizarMarcaVehiculo(marcaVehiculo);
                await Shell.Current.DisplayAlert("Exito", "Estado de la marca del vehiculo actualizado", "OK");
                await ObtenerTodasMarcas();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido obtener la marca del vehiculo", "OK");
                throw;
            }
        }

        public async Task CrearNuevaMarca()
        {
            MarcaVehiculo marcaVehiculoNueva = ArmarMarcaVehiculo(txtNombreMarca.Trim());
            try
            {
                await _vehiculoService.CrearMarca(marcaVehiculoNueva);
                await Shell.Current.DisplayAlert("Exito", "Marca del vehiculo creada exitosamente", "OK");
                await ObtenerTodasMarcas();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido crear la marca del vehiculo", "OK");
                throw;
            }
        }

        private MarcaVehiculo ArmarMarcaVehiculo(string nombreCategoria)
        {
            return new MarcaVehiculo
            {
                Nombre = nombreCategoria,
                Habilitado = "si",
            };
        }
    }
}
