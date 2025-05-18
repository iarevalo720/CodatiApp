using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IVehiculoService
    {
        public Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTO(string userId);
        public Task<IEnumerable<Vehiculo>> ObtenerVehiculos(string userId);
        public Task<Vehiculo> ObtenerVehiculoPorId(int id);
        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas();
        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca);
        public Task CrearVehiculo(Vehiculo vehiculo, string userId);
        public Task<IEnumerable<Categoria>> GetCategoria();
        public Task<IEnumerable<SubCategoriaDTO>> GetSubCategoria(int idCategoria);
        public Task ActualizarCategoria(Categoria categoria);
        public Task<Categoria?> ObtenerCategoriaPorId(int id);
        public Task<IEnumerable<SubCategoria>> ObtenerSubCategoriasPorCategoriaId(int categoriaId);
        public Task ActualizarSubCategoria(SubCategoria subCategoria);
        public Task CrearCategoria(Categoria categoria);
        public Task CrearSubCategoria(SubCategoria subCategoria);
    }
}
