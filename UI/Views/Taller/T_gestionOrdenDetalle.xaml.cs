using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(nroOrdenDetalle), nameof(nroOrdenDetalle))]
public partial class T_gestionOrdenDetalle : ContentPage
{
    private readonly T_gestionOrdenDetalleViewModel _t_gestionOrdenDetalleViewModel;
    public int nroOrdenDetalle { get; set; }

    public T_gestionOrdenDetalle(T_gestionOrdenDetalleViewModel gestionOrdenDetalleViewModel)
    {
        InitializeComponent();
        _t_gestionOrdenDetalleViewModel = gestionOrdenDetalleViewModel;
        BindingContext = _t_gestionOrdenDetalleViewModel;
        // Fix: Await the asynchronous method to ensure proper execution flow
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((T_gestionOrdenDetalleViewModel)BindingContext).CargarOrdenDetalleCompletoAsync(nroOrdenDetalle);
    }

    private void BtnEnviarObservacion(object sender, EventArgs e)
    {
        ((T_gestionOrdenDetalleViewModel)BindingContext).EnviarObservacion(nroOrdenDetalle);
    }
}
