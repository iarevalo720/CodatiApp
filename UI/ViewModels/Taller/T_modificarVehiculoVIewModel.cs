using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_modificarVehiculoVIewModel
    {
        private readonly IVehiculoService _vehiculoService;
        public Vehiculo Vehiculo { get; set; } = new Vehiculo();
        public string TransmisionSelected { get; set; }
        public string MarcaSelected { get; set; }
        public string ServicioSelected { get; set; }
        public List<string> ListaTransmision { get; set; }
        public T_modificarVehiculoVIewModel(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
            TransmisionSelected = string.Empty;

            ListaTransmision = new List<string>
            {
                "AUTOMATICO",
                "MANUAL"
            };
        }

        public async Task ObtenervehiculoPorId(int id)
        {
            Vehiculo = await _vehiculoService.ObtenerVehiculoPorId(id);
            TransmisionSelected = Vehiculo.Transmision;
            //MarcaSelected = Vehiculo.
        }
    }
}
