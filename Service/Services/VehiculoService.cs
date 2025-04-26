using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Data;

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

        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas()
        {
            var marcas = _vehiculoRepository.ObtenerMarcas();
            return marcas;
        }

        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca)
        {
            var modelos = _vehiculoRepository.ObtenerModelosPorMarca(idMarca);
            return modelos;
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
    }
}
