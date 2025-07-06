using QuestPDF.Fluent;
using UI.Utilities;
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
        int nroOrdenDetalle = ObtenerIdOrdenDetalle(sender);
        var ruta = $"{nameof(T_gestionOrdenDetalle)}?nroOrdenDetalle={nroOrdenDetalle}";
        await Shell.Current.GoToAsync(ruta);
    }

    private async void btnCrearComprobante(object sender, EventArgs e)
    {
        await _t_ordenDetalleViewModel.GenerarComprobante();
    }

    private async void btnFinalizarOrden(object sender, EventArgs e)
    {
        await _t_ordenDetalleViewModel.FinalizarOrden(nroOrden);
    }

    private int ObtenerIdOrdenDetalle(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent;
        return int.Parse(((Label)grid.Children.FirstOrDefault()).Text.Substring(1));
    }
}
