using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Globalization;
namespace Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetCantidadOrdenesPendientes()
        {
            return await _context.Ordenes.AsNoTracking().CountAsync(o => o.Estado == "A_VERIFICAR");
        }

        public async Task<Orden?> GetOrdenById(int idOrden)
        {
            var orden = await _context.Ordenes.Where(o => o.Id == idOrden).FirstOrDefaultAsync();
            return orden;
        }

        public async Task<IEnumerable<OrdenResumenDTO>> GetOrdenResumen()
        {
            try
            {
                var listadoOrdenReumen = await _context.Ordenes.AsNoTracking()
                    .Include(o => o.OrdenDetalles)
                        .ThenInclude(od => od.SubCategoria)
                    .Include(o => o.Vehiculo)
                        .ThenInclude(v => v.ModeloVehiculo)
                    .Select(o => new OrdenResumenDTO
                    {
                        NroOrden = o.Id.ToString(),
                        Estado = o.Estado,
                        Observacion = o.ObservacionCliente,
                        SubCategoria = o.OrdenDetalles.Select(od => od.SubCategoria.Nombre).ToList(),
                        ResumenVehiculo = o.Vehiculo.ModeloVehiculo.Nombre + " - " + o.Vehiculo.Anio
                    })
                    .ToListAsync();

                return listadoOrdenReumen;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task GuardarOrden(Orden orden)
        {
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task CrearOrden(Orden orden)
        {
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarHistorialOrdenCabecera(HistorialOrden historial)
        {
            await _context.HistorialOrdenes.AddAsync(historial);
            await _context.SaveChangesAsync();
        }

        public async Task<OrdenCompletoDTO?> GetOrdenCompleto(int idOrden)
        {
            try
            {
                var orden = await _context.Ordenes
                    .Include(o => o.OrdenDetalles)
                        .ThenInclude(od => od.SubCategoria)
                    .Include(o => o.HistorialOrdenes)
                    .Include(o => o.Usuario)
                    .FirstOrDefaultAsync(o => o.Id == idOrden);

                if (orden == null)
                {
                    return null;
                }

                Timbrado timbrado = await _context.Timbrados.Where(t => t.NumeroTimbrado == orden.NumeroTimbrado).FirstOrDefaultAsync();

                var ordenCompleto = new OrdenCompletoDTO
                {
                    OrdenId = orden.Id,
                    NombreUsuario = orden.Usuario?.Name,
                    NroDocumento = orden.Usuario?.NroDocumento,
                    NumeroFactura = orden.NumeroFactura,
                    NumeroTimbrado = orden.NumeroTimbrado,
                    EstadoOrden = orden.Estado,
                    FechaFinTimbrado = timbrado.FechaFin,
                    FechaFinalizacionOrden = orden.FechaFinalizacion,
                    ListaOrdenDetalleResumenes = orden.OrdenDetalles.Select(od => new OrdenDetalleResumen
                    {
                        Id = od.Id,
                        OrdenDetalleName = od.SubCategoria?.Nombre,
                        OrdenDetalleMonto = od.Precio,
                        OrdenDetalleEstado = od.Estado
                    }).ToList(),
                    ListaHistorialOrden = orden.HistorialOrdenes?
                        .Select(h => new HistorialOrdenDTO
                        {
                            Id = h.Id,
                            Fecha = h.Fecha,
                            Hora = h.Hora,
                            Descripcion = h.Descripcion,
                            OrdenId = h.OrdenId,
                            IdUsuario = h.IdUsuario
                        }).ToList() ?? new List<HistorialOrdenDTO>() // Handle null case
                };

                return ordenCompleto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task GuardarOrdenDetalle(OrdenDetalle ordenDetalle)
        {
            _context.OrdenDetalles.Update(ordenDetalle);
            await _context.SaveChangesAsync();
        }

        public async Task CrearOrdenDetalle(OrdenDetalle ordenDetalle)
        {
            await _context.OrdenDetalles.AddAsync(ordenDetalle);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarOrdenDetalleHistorial(OrdenDetalleHistorial ordenDetalleHistorial)
        {
            try
            {
                await _context.OrdenDetalleHistoriales.AddAsync(ordenDetalleHistorial);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la observacion");
            }
        }

        public async Task<OrdenDetalle?> GetOrdenDetalle(int ordenDetalleId)
        {
            return await _context.OrdenDetalles.Where(od => od.Id == ordenDetalleId).FirstOrDefaultAsync();
        }

        public async Task<OrdenDetalleCompletoDTO> GetOrdenDetalleCompleto(int idOrdenDetalle)
        {
            var ordenDetalle = await _context.OrdenDetalles
                .Include(od => od.OrdenDetalleHistorial)
                .Include(od => od.SubCategoria)
                .FirstOrDefaultAsync(od => od.Id == idOrdenDetalle);

            if (ordenDetalle == null || ordenDetalle.OrdenDetalleHistorial == null)
            {
                throw new ArgumentNullException(nameof(ordenDetalle), "OrdenDetalle or its OrdenDetalleHistorial is null.");
            }

            var ordenDetalleCompletoDto = new OrdenDetalleCompletoDTO
            {
                OrdenDetalleId = ordenDetalle.Id,
                OrdenCabeceraId = ordenDetalle.OrdenId,
                OrdenDetalleNombre = ordenDetalle.SubCategoria?.Nombre,
                Estado = ordenDetalle.Estado,
                Monto = ordenDetalle.Precio,
                ListaOrdenDetallesHistorial = new ObservableCollection<OrdenDetalleHistorial>(
                    ordenDetalle.OrdenDetalleHistorial.Select(odh => new OrdenDetalleHistorial
                    {
                        Id = odh.Id,
                        Descripcion = odh.Descripcion,
                        Fecha = odh.Fecha,
                        Hora = odh.Hora,
                        OrdenDetalleId = odh.OrdenDetalleId,
                        NombreUsuario = odh.NombreUsuario
                    }))
            };

            return ordenDetalleCompletoDto;
        }

        public async Task CrearTimbrado(Timbrado timbrado)
        {
            await _context.Timbrados.AddAsync(timbrado);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Timbrado>> ObtenerTimbrados()
        {
            return await _context.Timbrados.ToListAsync();
        }

        public async Task ActualizarTimbrado(Timbrado timbrado)
        {
            _context.Timbrados.Update(timbrado);
            await _context.SaveChangesAsync();
        }

        public async Task<Timbrado?> ObtenerTimbradoSeleccionado()
        {
            return await _context.Timbrados
                .Where(t => t.TimbradoSeleccionado == "si")
                .FirstOrDefaultAsync();
        }

        // En Data/Repository/OrderRepository.cs
        public async Task<InformeOrdenesDTO> ObtenerInformeOrdenes(DateTime fechaInicio, DateTime fechaFin)
        {
            // Convertir fechas a formato string para comparar con el formato de la base de datos
            string fechaInicioStr = fechaInicio.ToString("dd/MM/yyyy");
            string fechaFinStr = fechaFin.ToString("dd/MM/yyyy");

            var ordenes = await _context.Ordenes
                .Include(o => o.OrdenDetalles)
                    .ThenInclude(od => od.SubCategoria)
                .ToListAsync();

            // Filtrar por rango de fechas
            var ordenesFiltradas = ordenes.Where(o =>
            {
                if (DateTime.TryParseExact(o.FechaCreacion, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaCreacion))
                {
                    return fechaCreacion >= fechaInicio && fechaCreacion <= fechaFin;
                }
                return false;
            }).ToList();

            var totalOrdenes = ordenesFiltradas.Count;
            var ordenesEnProceso = ordenesFiltradas.Count(o => o.Estado == "EN_PROCESO");
            var ordenesRechazadas = ordenesFiltradas.Count(o => o.Estado == "RECHAZADO");
            var ordenesFinalizadas = ordenesFiltradas.Count(o => o.Estado == "FINALIZADO");

            // Contar servicios más solicitados
            var serviciosCount = ordenesFiltradas
                .SelectMany(o => o.OrdenDetalles)
                .GroupBy(od => od.SubCategoria.Nombre)
                .Select(g => new ServicioFrecuenciaDTO
                {
                    Servicio = g.Key,
                    Frecuencia = g.Count()
                })
                .OrderByDescending(s => s.Frecuencia)
                .ToList();

            return new InformeOrdenesDTO
            {
                TotalOrdenes = totalOrdenes,
                OrdenesEnProceso = ordenesEnProceso,
                OrdenesRechazadas = ordenesRechazadas,
                OrdenesFinalizadas = ordenesFinalizadas,
                ServiciosMasSolicitados = serviciosCount
            };
        }
    }
}
