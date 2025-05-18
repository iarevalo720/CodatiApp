using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Service.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoService(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTO(string userId)
        {
            return _vehiculoRepository.ObtenerVehiculosDTO(userId);
        }

        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcasHabilitadas()
        {
            return _vehiculoRepository.ObtenerMarcasHabilitadas();
        }
        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas()
        {
            return _vehiculoRepository.ObtenerMarcas();
        }

        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca)
        {
            return _vehiculoRepository.ObtenerModelosPorMarca(idMarca);
        }

        public async Task CrearVehiculo(Vehiculo vehiculoInsert, string userId)
        {
            Vehiculo vehiculo = GenerarVehiculo(vehiculoInsert, userId);
            await _vehiculoRepository.AddVehiculo(vehiculo);
        }

        private Vehiculo GenerarVehiculo(Vehiculo vehiculoInsert, string userId)
        {
            return new Vehiculo
            {
                Matricula = vehiculoInsert.Matricula,
                Color = vehiculoInsert.Color,
                Anio = vehiculoInsert.Anio,
                Transmision = vehiculoInsert.Transmision,
                Kilometraje = vehiculoInsert.Kilometraje,
                ModeloVehiculoId = vehiculoInsert.ModeloVehiculo.ModeloVehiculoId,
                FechaAlta = DateTime.Now.ToString("dd/MM/yyyy"),
                UserId = userId
            };
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerVehiculos(string userId)
        {
            return await _vehiculoRepository.ObtenerVehiculos(userId);
        }

        public async Task<IEnumerable<Categoria>> GetCategoria()
        {
            return await _vehiculoRepository.GetCategoria();
        }

        public async Task<IEnumerable<SubCategoriaDTO>> GetSubCategoria(int idCategoria)
        {
            return await _vehiculoRepository.GetSubCategoria(idCategoria);
        }

        public Task<Vehiculo> ObtenerVehiculoPorId(int id)
        {
            return _vehiculoRepository.ObtenerVehiculoPorId(id);
        }

        public Task ActualizarCategoria(Categoria categoria)
        {
            return _vehiculoRepository.ActualizarCategoria(categoria);
        }

        public Task<Categoria?> ObtenerCategoriaPorId(int id)
        {
            return _vehiculoRepository.ObtenerCategoriaPorId(id);
        }

        public Task<IEnumerable<SubCategoria>> ObtenerSubCategoriasPorCategoriaId(int categoriaId)
        {
            return _vehiculoRepository.GetSubCategoriasPorCategoriaId(categoriaId);
        }

        public async Task ActualizarSubCategoria(SubCategoria subCategoria)
        {
            await _vehiculoRepository.ActualizarSubCategoria(subCategoria);
        }

        public async Task CrearCategoria(Categoria categoria)
        {
            await _vehiculoRepository.CrearCategoria(categoria);
        }

        public async Task CrearSubCategoria(SubCategoria subCategoria)
        {
            await _vehiculoRepository.CrearSubCategoria(subCategoria);
        }

        public async Task ActualizarMarcaVehiculo(MarcaVehiculo marcaVehiculo)
        {
            await _vehiculoRepository.ActualizarMarcaVehiculo(marcaVehiculo);
        }

        public async Task ActualizarModeloVehiculo(ModeloVehiculo modeloVehiculo)
        {
            await _vehiculoRepository.ActualizarModeloVehiculo(modeloVehiculo);
        }

        public async Task CrearMarca(MarcaVehiculo marcaVehiculo)
        {
            await _vehiculoRepository.CrearMarca(marcaVehiculo);
        }

        public async Task CrearModelo(ModeloVehiculo modeloVehiculo)
        {
            await _vehiculoRepository.CrearModelo(modeloVehiculo);
        }
    }
}
