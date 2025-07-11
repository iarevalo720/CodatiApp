﻿using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly AppDbContext _context;

        public VehiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTO(string userId)
        {
            var vehiculo = await _context.Vehiculos
                .Where(v => v.UserId == userId)
                .Include(v => v.ModeloVehiculo)
                .ThenInclude(mv => mv.MarcaVehiculo)
                .Select(v => new VehiculoDTO
                {
                    Id = v.Id,
                    Matricula = v.Matricula,
                    Anio = v.Anio,
                    Color = v.Color,
                    FechaAlta = v.FechaAlta,
                    Kilometraje = v.Kilometraje,
                    Transmision = v.Transmision,
                    ModeloVehiculoNombre = v.ModeloVehiculo.Nombre,
                    MarcaVehiculoNombre = v.ModeloVehiculo.MarcaVehiculo.Nombre,
                    Habilitado = v.Habilitado
                })
                .ToListAsync();

            return vehiculo;
        }

        public async Task<IEnumerable<MarcaVehiculo>> ObtenerMarcasHabilitadas()
        {
            return await _context.MarcaVehiculos.Where(ma => ma.Habilitado == "si").ToListAsync();
        }

        public async Task<IEnumerable<ModeloVehiculo>> ObtenerModelosPorMarca(int idMarca)
        {
            return await _context.ModeloVehiculos.Where(mo => mo.MarcaVehiculoId == idMarca).ToListAsync();
        }

        public async Task AddVehiculo(Vehiculo vehiculo)
        {
            await _context.Vehiculos.AddAsync(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerVehiculos(string userId)
        {
            return await _context.Vehiculos.Where(v => v.UserId == userId).Where(v => v.Habilitado == "si").ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoria()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasHabilitadas()
        {
            return await _context.Categoria.Where(c => c.Habilitado == "si").ToListAsync();
        }

        public async Task<IEnumerable<SubCategoria>> GetSubCategoria(int idCategoria)
        {
            return await _context.SubCategoria.ToListAsync();
        }

        public async Task<Vehiculo> ObtenerVehiculoPorId(int id)
        {
            return await _context.Vehiculos.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task ActualizarCategoria(Categoria categoria)
        {
            _context.Categoria.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria?> ObtenerCategoriaPorId(int id) => await _context.Categoria.Where(c => c.CategoriaId == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<SubCategoria>> GetSubCategoriasPorCategoriaId(int idCategoria)
        {
            return await _context.SubCategoria
                .Where(x => x.CategoriaId == idCategoria)
                .ToListAsync();
        }

        public async Task ActualizarSubCategoria(SubCategoria subCategoria)
        {
            _context.SubCategoria.Update(subCategoria);
            await _context.SaveChangesAsync();
        }

        public async Task CrearCategoria(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task CrearSubCategoria(SubCategoria subCategoria)
        {
            await _context.SubCategoria.AddAsync(subCategoria);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas()
        {
            return await _context.MarcaVehiculos.ToListAsync();
        }

        public async Task ActualizarMarcaVehiculo(MarcaVehiculo marcaVehiculo)
        {
            _context.MarcaVehiculos.Update(marcaVehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarModeloVehiculo(ModeloVehiculo modeloVehiculo)
        {
            _context.ModeloVehiculos.Update(modeloVehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task CrearMarca(MarcaVehiculo marcaVehiculo)
        {
            await _context.MarcaVehiculos.AddAsync(marcaVehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task CrearModelo(ModeloVehiculo modeloVehiculo)
        {
            await _context.ModeloVehiculos.AddAsync(modeloVehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ModeloVehiculo>> ObtenerModelosHabilitadosPorMarca(int idMarca)
        {
            return await _context.ModeloVehiculos.Where(mo => mo.MarcaVehiculoId == idMarca).Where(mo => mo.Habilitado == "si").ToListAsync();
        }

        public async Task<IEnumerable<SubCategoriaDTO>> GetSubCategoriasHabilitadas(int idCategoria)
        {
            return await _context.SubCategoria
            .Where(x => x.CategoriaId == idCategoria)
            .Select(x => new SubCategoriaDTO
            {
                SubCategoriaId = x.Id,
                Nombre = x.Nombre,
            }).ToListAsync();
        }

        public async Task ActualizarVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehiculoDTO?>> ObtenerVehiculosDTOHabilitados(string userId)
        {
            var vehiculo = await _context.Vehiculos
                .Where(v => v.UserId == userId && v.Habilitado.ToLower() == "si")
                .Include(v => v.ModeloVehiculo)
                .ThenInclude(mv => mv.MarcaVehiculo)
                .Select(v => new VehiculoDTO
                {
                    Id = v.Id,
                    Matricula = v.Matricula,
                    Anio = v.Anio,
                    Color = v.Color,
                    FechaAlta = v.FechaAlta,
                    Kilometraje = v.Kilometraje,
                    Transmision = v.Transmision,
                    ModeloVehiculoNombre = v.ModeloVehiculo.Nombre,
                    MarcaVehiculoNombre = v.ModeloVehiculo.MarcaVehiculo.Nombre,
                    Habilitado = v.Habilitado
                })
                .ToListAsync();

            return vehiculo;
        }

        public async Task<VehiculoDTO?> ObtenerVehiculoDTOById(int id)
        {
            var vehiculo = await _context.Vehiculos
                .Where(v => v.Id == id)
                .Include(v => v.ModeloVehiculo)
                .ThenInclude(mv => mv.MarcaVehiculo)
                .Select(v => new VehiculoDTO
                {
                    Id = v.Id,
                    Matricula = v.Matricula,
                    Anio = v.Anio,
                    Color = v.Color,
                    FechaAlta = v.FechaAlta,
                    Kilometraje = v.Kilometraje,
                    Transmision = v.Transmision,
                    ModeloVehiculoNombre = v.ModeloVehiculo.Nombre,
                    MarcaVehiculoNombre = v.ModeloVehiculo.MarcaVehiculo.Nombre,
                    Habilitado = v.Habilitado
                })
                .FirstOrDefaultAsync();

            return vehiculo;
        }

        public async Task<int> ObtenerIdMarcaByModeloId(int idModelo)
        {
            return await _context.ModeloVehiculos
                .Where(m => m.Id == idModelo)
                .Select(m => m.MarcaVehiculoId)
                .FirstOrDefaultAsync();
        }
    }
}
