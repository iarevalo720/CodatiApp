using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_ordenes : ContentPage
{
	private readonly T_ordenesViewModel _t_ordenesViewModel;
    public T_ordenes(T_ordenesViewModel t_OrdenesViewModel)
	{
		InitializeComponent();
        _t_ordenesViewModel = t_OrdenesViewModel;
        BindingContext = _t_ordenesViewModel;
    }

    private async void BtnAceptarOrden(object sender, EventArgs e)
    {
        if (await DisplayAlert("Aceptar orden", "Esta seguro de aceptar esta orden?", "Si", "No"))
        {
            int orden = ObtenerNroOrden(sender);
            bool esOrdenAceptado = await _t_ordenesViewModel.AceptarOrden(orden);

            if (esOrdenAceptado) await DisplayAlert("Exito", "La orden fue aceptada exitosamente", "OK");
            else await DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo mas tarde", "OK");
        }
    }

    private async void BtnRechazarOrden(object sender, EventArgs e)
    {
        string mensajeRechazo = await DisplayPromptAsync("Rechazar orden", "Esta seguro de rechazar esta orden? Asigne una observacion de rechazo");
        if (mensajeRechazo != null)
        {
            int orden = ObtenerNroOrden(sender);
            bool esOrdenRechazado = await _t_ordenesViewModel.RechazarOrden(orden, mensajeRechazo);
            if (esOrdenRechazado) await DisplayAlert("Exito", "La orden fue rechazada exitosamente", "OK");
            else await DisplayAlert("Error", "Ha ocurrido un error, por favor intentelo mas tarde", "OK");
        }
    }

    private async void BtnDetalleOrden(object sender, EventArgs e)
    {
        int nroOrden = ObtenerNroOrden(sender);
        var ruta = $"{nameof(T_ordenDetalle)}?nroOrden={nroOrden}";
        await Shell.Current.GoToAsync(ruta);
    }

    private int ObtenerNroOrden(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)grid.Children.FirstOrDefault()).Text.Substring(7));
    }
}