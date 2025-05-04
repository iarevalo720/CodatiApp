using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(nroOrden), nameof(nroOrden))]
public partial class T_crearComprobante : ContentPage
{
	private readonly T_crearComprobanteViewModel _viewModel;
    public int nroOrden { get; set; }

    public T_crearComprobante(T_crearComprobanteViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerDatosComprobante(nroOrden);
    }
}