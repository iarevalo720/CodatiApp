using Core.DTOs;
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
                    VehiculoId = v.VehiculoId,
                    Matricula = v.Matricula,
                    Anio = v.Anio,
                    Color = v.Color,
                    FechaAlta = v.FechaAlta,
                    Kilometraje = v.Kilometraje,
                    Transmision = v.Transmision,
                    ModeloVehiculoNombre = v.ModeloVehiculo.Nombre,
                    MarcaVehiculoNombre = v.ModeloVehiculo.MarcaVehiculo.Nombre
                })
                .ToListAsync();

            return vehiculo;
        }

        public async Task<IEnumerable<MarcaVehiculo>> ObtenerMarcas()
        {
            return await _context.MarcaVehiculos.ToListAsync();
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
            return await _context.Vehiculos.Where(v => v.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoria()
        {
            return await _context.Categoria.ToListAsync();
        }
        public async Task<IEnumerable<SubCategoriaDTO>> GetSubCategoria(int idCategoria)
        {
            return await _context.SubCategoria
                .Where(x => x.CategoriaId == idCategoria)
                .Select(x => new SubCategoriaDTO
                {
                    SubCategoriaId = x.SubCategoriaId,
                    Nombre = x.Nombre,
                }).ToListAsync();
        }

        public async Task<Vehiculo> ObtenerVehiculoPorId(int id)
        {
            return await _context.Vehiculos.Where(v => v.VehiculoId == id).FirstOrDefaultAsync();
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
    }
}
