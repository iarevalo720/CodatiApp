﻿using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IVehiculoService
    {
        public Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTO(string userId);
        public Task<VehiculoDTO?> ObtenerVehiculosDTOById(int id);
        public Task<int> ObtenerMarcaIdByModeloId(int modeloId);
        public Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTOHabilitados(string userId);
        public Task<IEnumerable<Vehiculo>> ObtenerVehiculos(string userId);
        public Task<Vehiculo> ObtenerVehiculoPorId(int id);
        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcasHabilitadas();
        public Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas();
        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca);
        public Task<IEnumerable<ModeloVehiculo>> ObtenerModelosHabilitadosPorMarca(int idMarca);
        public Task CrearVehiculo(Vehiculo vehiculo, string userId);
        public Task<IEnumerable<Categoria>> GetCategoria();
        public Task<IEnumerable<Categoria>> GetCategoriasHabilitadas();
        public Task<IEnumerable<SubCategoria>> GetSubCategoria(int idCategoria);
        public Task<IEnumerable<SubCategoriaDTO>> GetSubCategoriasHabilitadas(int idCategoria);
        public Task ActualizarVehiculo(Vehiculo vehiculo);
        public Task ActualizarCategoria(Categoria categoria);
        public Task<Categoria?> ObtenerCategoriaPorId(int id);
        public Task<IEnumerable<SubCategoria>> ObtenerSubCategoriasPorCategoriaId(int categoriaId);
        public Task ActualizarSubCategoria(SubCategoria subCategoria);
        public Task CrearCategoria(Categoria categoria);
        public Task CrearSubCategoria(SubCategoria subCategoria);
        public Task ActualizarMarcaVehiculo(MarcaVehiculo marcaVehiculo);
        public Task ActualizarModeloVehiculo(ModeloVehiculo modeloVehiculo);
        public Task CrearMarca(MarcaVehiculo marcaVehiculo);
        public Task CrearModelo(ModeloVehiculo modeloVehiculo);
    }
}
