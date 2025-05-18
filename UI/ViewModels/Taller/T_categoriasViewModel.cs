using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_categoriasViewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public IEnumerable<Categoria> ListaCategorias { get; set; } = Enumerable.Empty<Categoria>();

        public T_categoriasViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task CambiarNombreCategoria(int categoriaId)
        {
            string respuesta = await Shell.Current.DisplayPromptAsync("Renombrar categoria", "Desea renombrar la categoria? Asigne su nuevo nombre", "Renombrar", "Cancelar");
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                Categoria? categoria = await ObtenerCategoriaPorId(categoriaId);

                if (categoria == null)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido obtener la categoria", "OK");
                    return;
                }

                categoria.Nombre = respuesta;
                try
                {
                    await _vehiculoService.ActualizarCategoria(categoria);
                    await Shell.Current.DisplayAlert("Exito", "Se ha actualizar exitosamente", "OK");
                    await ObtenerCategorias();
                }
                catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Error", "No se ha podido actualizar la categoria", "OK");
                    throw;
                }
            }
        }

        public async Task CambiarEstadoCategoria(int categoriaId)
        {
            try
            {
                Categoria? categoria = ListaCategorias.FirstOrDefault(c => c.CategoriaId == categoriaId);
                if (categoria == null) throw new Exception("Categoria no encontrada");
                categoria.Habilitado = categoria.Habilitado == "si" ? "no" : "si";
                await _vehiculoService.ActualizarCategoria(categoria);
                await Shell.Current.DisplayAlert("Exito", "Estado de la categoria actualizado", "OK");
                await ObtenerCategorias();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido obtener la categoria", "OK");
                throw;
            }
        }


        public async Task ObtenerCategorias()
        {
            ListaCategorias = await _vehiculoService.GetCategoria();
        }

        private async Task<Categoria?> ObtenerCategoriaPorId(int id)
        {
            return await _vehiculoService.ObtenerCategoriaPorId(id);
        }
    }
}
