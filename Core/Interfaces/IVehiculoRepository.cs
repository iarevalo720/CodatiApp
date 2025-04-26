using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IVehiculoRepository
    {
        public Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTO(string userId);
        public Task<IEnumerable<Vehiculo>> ObtenerVehiculos(string userId);
        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas();
        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca);
        public Task AddVehiculo(Vehiculo vehiculo);
        public Task<IEnumerable<Categoria>> GetCategoria();
        public Task<IEnumerable<SubCategoriaDTO>> GetSubCategoria(int idCategoria);
    }
}
