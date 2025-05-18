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
        public string txtNombreCategoria { get; set; } = string.Empty;

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
                    await Shell.Current.DisplayAlert("Exito", "Se ha actualizado exitosamente", "OK");
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
            txtNombreCategoria = string.Empty;
        }

        public async Task CrearNuevaCategoria()
        {
            Categoria categoria = ArmarCategoria(txtNombreCategoria.Trim());
            try
            {
                await _vehiculoService.CrearCategoria(categoria);
                await Shell.Current.DisplayAlert("Exito", "Categoria creada exitosamente", "OK");
                await ObtenerCategorias();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido crear la categoria", "OK");
                throw;
            }
        }

        private async Task<Categoria?> ObtenerCategoriaPorId(int id)
        {
            return await _vehiculoService.ObtenerCategoriaPorId(id);
        }

        private Categoria ArmarCategoria(string nombreCategoria)
        {
            return new Categoria
            {
                Nombre = nombreCategoria,
                Habilitado = "si",
            };
        }
    }
}
