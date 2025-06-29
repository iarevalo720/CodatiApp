using Core.Entities;
using Core.Interfaces;
using PropertyChanged;
using Service.Services;

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
                    await Shell.Current.DisplayAlert("Éxito", "Timbrado regitrado correctamente", "OK");
                    await ObtenerTimbrados();
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

        public async Task CambiarEstadoTimbrado(int timbradoId)
        {
            try
            {
                Timbrado? timbrado = ListaTimbrados.FirstOrDefault(c => c.Id == timbradoId);
                if (timbrado == null) throw new Exception("timbrado no encontrado");
                timbrado.EsHabilitado = timbrado.EsHabilitado == "si" ? "no" : "si";
                await _ordenService.ActualizarTimbrado(timbrado);
                await Shell.Current.DisplayAlert("Exito", "Estado del timbrado actualizado exitosamente", "OK");
                await ObtenerTimbrados();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo más tarde", "OK");
                throw;
            }
        }

        public async Task SeleccionarTimbrado(int timbradoId)
        {
            Timbrado? timbrado = ListaTimbrados.FirstOrDefault(c => c.Id == timbradoId);
            if (timbrado == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha encontrado el timbrado.", "OK");
                return;
            }

            if (!await EsTimbradoValido(timbrado)) return;

            foreach (var timbradoItem in ListaTimbrados)
            {
                timbradoItem.TimbradoSeleccionado = "no";
                await _ordenService.ActualizarTimbrado(timbradoItem);
            }

            timbrado.TimbradoSeleccionado = "si";
            await _ordenService.ActualizarTimbrado(timbrado);

            await Shell.Current.DisplayAlert("Éxito", "Timbrado seleccionado correctamente", "OK");
            await ObtenerTimbrados();
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

        private async Task<bool> EsTimbradoValido(Timbrado timbrado)
        {
            DateTime fechaFin = DateTime.ParseExact(timbrado.FechaFin, "dd/MM/yyyy", null);
            if (fechaFin < DateTime.Now)
            {
                await Shell.Current.DisplayAlert("Información", "El timbrado seleccionado está expirado, por favor seleccione o registre otro timbrado", "OK");

                return false; // Timbrado expirado
            }

            if (timbrado.EsHabilitado == "no")
            {
                await Shell.Current.DisplayAlert("Infomración", "El timbrado seleccionado no se encuentra habilitado, por favor seleccione o registre otro timbrado", "OK");
                return false; // Timbrado Inhabilitado
            }

            if (timbrado.NumeroSecuencialActual >= timbrado.NumeroSecuencialMaximo)
            {
                await Shell.Current.DisplayAlert("Infomración", "El timbrado ha superado el cupo del número secuencial máximo, por favor seleccione o registre otro timbrado", "OK");
                return false; // Cupo completo del timbrado
            }

            return true;
        }
    }
}
