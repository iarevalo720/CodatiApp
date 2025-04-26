using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(nroOrden), nameof(nroOrden))]
public partial class T_ordenDetalle : ContentPage
{
    private readonly T_ordenDetalleViewModel _t_ordenDetalleViewModel;
    public int nroOrden { get; set; }
    public T_ordenDetalle(T_ordenDetalleViewModel t_OrdenDetalleViewModel)
    {
        InitializeComponent();
        _t_ordenDetalleViewModel = t_OrdenDetalleViewModel;
        BindingContext = _t_ordenDetalleViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((T_ordenDetalleViewModel)BindingContext).CargarOrdenCompletoAsync(nroOrden);
    }

    private async void BtnGuardarEstadoOrdenCabecera(object sender, EventArgs e)
    {
        await ((T_ordenDetalleViewModel)BindingContext).ActualizarOrdenCabecera(nroOrden);
    }

    private async void btnIrGestionordenDetalle(object sender, EventArgs e)
    {
        int nroOrdenDetalle = int.Parse(txtOrdenId.Text.Substring(7));
        var ruta = $"{nameof(T_gestionOrdenDetalle)}?nroOrdenDetalle={nroOrdenDetalle}";
        await Shell.Current.GoToAsync(ruta);
    }
}
