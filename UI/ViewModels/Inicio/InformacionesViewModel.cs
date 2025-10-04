// UI/ViewModels/Inicio/InformacionesViewModel.cs
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace UI.ViewModels.Inicio
{
    [AddINotifyPropertyChangedInterface]
    public class InformacionesViewModel
    {
        private readonly IVehiculoService _vehiculoService;

        public ObservableCollection<MarcaConModelos> MarcasConModelos { get; set; } = new();
        public ObservableCollection<CategoriaConServicios> CategoriasConServicios { get; set; } = new();
        public bool EstaCargando { get; set; }

        public InformacionesViewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task CargarInformaciones()
        {
            try
            {
                EstaCargando = true;

                // Cargar marcas habilitadas
                var marcas = await _vehiculoService.ObtenerMarcasHabilitadas();

                // Cargar categorías habilitadas
                var categorias = await _vehiculoService.GetCategoriasHabilitadas();

                // Limpiar y cargar marcas con modelos
                MarcasConModelos.Clear();
                foreach (var marca in marcas)
                {
                    var modelos = await _vehiculoService.ObtenerModelosHabilitadosPorMarca(marca.Id);
                    MarcasConModelos.Add(new MarcaConModelos
                    {
                        Marca = marca,
                        Modelos = new ObservableCollection<ModeloVehiculo>(modelos)
                    });
                }

                // Limpiar y cargar categorías con servicios
                CategoriasConServicios.Clear();
                foreach (var categoria in categorias)
                {
                    var servicios = await _vehiculoService.GetSubCategoriasHabilitadas(categoria.CategoriaId);
                    CategoriasConServicios.Add(new CategoriaConServicios
                    {
                        Categoria = categoria,
                        Servicios = new ObservableCollection<SubCategoriaDTO>(servicios)
                    });
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "No se pudieron cargar las informaciones", "OK");
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }

    public class MarcaConModelos
    {
        public MarcaVehiculo Marca { get; set; }
        public ObservableCollection<ModeloVehiculo> Modelos { get; set; }
    }

    public class CategoriaConServicios
    {
        public Categoria Categoria { get; set; }
        public ObservableCollection<SubCategoriaDTO> Servicios { get; set; }
    }
}