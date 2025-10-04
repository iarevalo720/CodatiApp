// En UI/ViewModels/Taller/T_informeViewModel.cs
using Core.DTOs;
using Core.Interfaces;
using PropertyChanged;
using System.Windows.Input;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_informeViewModel
    {
        private readonly IOrderService _orderService;

        public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(-7);
        public DateTime FechaFin { get; set; } = DateTime.Now;

        public int TotalOrdenes { get; set; }
        public int OrdenesEnProceso { get; set; }
        public int OrdenesRechazadas { get; set; }
        public int OrdenesFinalizadas { get; set; }
        public List<ServicioFrecuenciaDTO> ServiciosMasSolicitados { get; set; } = new List<ServicioFrecuenciaDTO>();

        public bool MostrarResultados { get; set; }
        public bool IsBusy { get; set; } = false;
        public bool IsNoBusy => !IsBusy;

        public ICommand GenerarInformeCommand { get; }

        public T_informeViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            GenerarInformeCommand = new Command(async () => await GenerarInforme());
        }

        private async Task GenerarInforme()
        {
            if (FechaInicio > FechaFin)
            {
                await Shell.Current.DisplayAlert("Error", "La fecha de inicio no puede ser mayor a la fecha fin", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var informe = await _orderService.ObtenerInformeOrdenes(FechaInicio, FechaFin);

                TotalOrdenes = informe.TotalOrdenes;
                OrdenesEnProceso = informe.OrdenesEnProceso;
                OrdenesFinalizadas = informe.OrdenesFinalizadas;
                OrdenesRechazadas = informe.OrdenesRechazadas;
                ServiciosMasSolicitados = informe.ServiciosMasSolicitados;

                Console.WriteLine($"Servicios encontrados: {ServiciosMasSolicitados?.Count}");

                MostrarResultados = true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo generar el informe: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}