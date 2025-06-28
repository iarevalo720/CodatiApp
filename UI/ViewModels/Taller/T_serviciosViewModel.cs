using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_serviciosViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public IEnumerable<SubCategoria> ListaServicios { get; set; } = Enumerable.Empty<SubCategoria>();
        public string txtNombreServicio { get; set; } = string.Empty;

        public T_serviciosViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }
        public async Task ObtenerServiciosPorCategoriaId(int categoriaId)
        {
            ListaServicios = await _vehiculoService.ObtenerSubCategoriasPorCategoriaId(categoriaId);
            txtNombreServicio = string.Empty;
        }

        public async Task CambiarEstadoServicio(int servicioId)
        {
            SubCategoria? subCategoria = ListaServicios.FirstOrDefault(x => x.Id == servicioId);

            if (subCategoria == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar el servicio", "OK");
                return;
            }

            subCategoria.Habilitado = subCategoria.Habilitado == "si" ? "no" : "si";

            await GuardarServicio(subCategoria);
        }

        public async Task CambiarNombreServicio(int servicioId)
        {
            string respuesta = await Shell.Current.DisplayPromptAsync("Renombrar servicio", "Desea renombrar el servicio? Asigne su nuevo nombre", "Renombrar", "Cancelar");
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                SubCategoria? subCategoria = ListaServicios.FirstOrDefault(x => x.Id == servicioId);
                if (subCategoria == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido obtener el servicio", "OK");
                    return;
                }
                subCategoria.Nombre = respuesta.Trim();
                await GuardarServicio(subCategoria);
            }
        }

        public async Task CrearNuevaSubCategoria(int categoriaId)
        {
            SubCategoria subCategoria = ArmarSubCategoria(txtNombreServicio.Trim(), categoriaId);
            try
            {
                await _vehiculoService.CrearSubCategoria(subCategoria);
                await Shell.Current.DisplayAlert("Exito", "Servicio creado exitosamente", "OK");
                await ObtenerServiciosPorCategoriaId(categoriaId);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido crear el servicio", "OK");
                throw;
            }
        }

        private async Task GuardarServicio(SubCategoria subCategoria)
        {
            try
            {
                await _vehiculoService.ActualizarSubCategoria(subCategoria);
                await Shell.Current.DisplayAlert("Exito", "Se ha actualizado exitosamente", "OK");
                await ObtenerServiciosPorCategoriaId(subCategoria.CategoriaId);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar el servicio", "OK");
                throw;
            }
        }

        private SubCategoria ArmarSubCategoria(string nombreSubCategoria, int categoriaId)
        {
            return new SubCategoria
            {
                Nombre = nombreSubCategoria.Trim(),
                Habilitado = "si",
                CategoriaId = categoriaId
            };
        }
    }
}
