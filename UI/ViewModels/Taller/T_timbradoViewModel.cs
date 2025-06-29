using Core.Entities;
using Core.Interfaces;
using PropertyChanged;

namespace UI.ViewModels.Taller
{
    [AddINotifyPropertyChangedInterface]
    public class T_timbradoViewModel
    {
        private IOrderService _ordenService;
        public IEnumerable<Timbrado> ListaTimbrados { get; set; } = new List<Timbrado>();
        public string txtNumeroTimbrado { get; set; }
        public string txtPuntoEstablecimiento { get; set; }
        public string txtPuntoExpedicion { get; set; }
        public string txtNumeroSecuencialMaximo { get; set; }
        public DateTime PickerFechaInicio { get; set; } = DateTime.Now;
        public DateTime PickerFechaFin { get; set; } = DateTime.Now.AddMonths(1);

        public T_timbradoViewModel(IOrderService ordenService)
        {
            _ordenService = ordenService;
            ListaTimbrados = new List<Timbrado>();
        }

        public async Task ObtenerTimbrados()
        {
            ListaTimbrados = await _ordenService.ObtenerTimbrados();
        }

        public async Task RegistrarTimbrado()
        {
            if (!SonCamposValidos())
            {
                await Shell.Current.DisplayAlert("Informacion", "Por favor complete todos los campos", "OK");
                return;
            }

            if (PickerFechaInicio > PickerFechaFin)
            {
                await Shell.Current.DisplayAlert("Picker", "la fecha inicio no puede ser mayor a fecha fin", "OK");
                return;
            }
            try
            {
                bool confirmar = await Shell.Current.DisplayAlert("Confirmación", "Está seguro de registrar un nuevo timbrado?", "Si", "No");
                if (!confirmar) return;

                try
                {
                    await _ordenService.GuardarTimbrado(txtNumeroTimbrado, txtPuntoEstablecimiento, txtPuntoExpedicion, txtNumeroSecuencialMaximo, PickerFechaInicio, PickerFechaFin);
                    await Shell.Current.DisplayAlert("Éxito", "Timbrado guardado correctamente", "OK");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", "ha ocurrido un error, por favor intentelo mas tarde", "Ok");
                    return;
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo guardar el timbrado: {ex.Message}", "OK");
            }
        }

        private bool SonCamposValidos()
        {
            if (String.IsNullOrWhiteSpace(txtNumeroTimbrado) ||
                String.IsNullOrWhiteSpace(txtPuntoEstablecimiento) ||
                String.IsNullOrWhiteSpace(txtPuntoExpedicion) ||
                String.IsNullOrWhiteSpace(txtNumeroSecuencialMaximo))
            {
                return false;
            }

            return true;
        }
    }
}
