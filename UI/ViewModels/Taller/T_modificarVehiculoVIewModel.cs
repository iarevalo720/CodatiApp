using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_modificarVehiculoVIewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public Vehiculo Vehiculo { get; set; } = new Vehiculo();
        public T_modificarVehiculoVIewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        public async Task ObtenervehiculoPorId(int id)
        {
            Vehiculo = await _vehiculoService.ObtenerVehiculoPorId(id);
        }
    }
}
