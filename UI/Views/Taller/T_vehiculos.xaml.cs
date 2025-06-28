using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_vehiculos : ContentPage
{
    private readonly T_vehiculosViewModel _viewModel;
    public T_vehiculos(T_vehiculosViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void BtnLimpiarCampos(object sender, EventArgs e)
    {
        _viewModel.LimpiarCampos();
    }

    private async void BtnBuscarVehiculo(object sender, EventArgs e)
    {
        await _viewModel.BtnBuscarVehiculo();
    }

    private async void BtnIrModificarVehiculo(object sender, EventArgs e)
    {
        int vehiculoId = ObtenerNroOrden(sender);
        var ruta = $"{nameof(T_modificarVehiculo)}?vehiculoId={vehiculoId}";
        await Shell.Current.GoToAsync(ruta);
    }

    private int ObtenerNroOrden(object sender)
    {
        VerticalStackLayout verticalStackLayout = (VerticalStackLayout)((Button)sender).Parent;
        return int.Parse(((Label)verticalStackLayout.Children.FirstOrDefault()).Text.Substring(1));
    }

    private async void BtnCambiarEstadoVehiculo(object sender, EventArgs e)
    {
        int vehiculoId = ObtenerNroOrden(sender);
        await _viewModel.CambiarEstadoVehiculo(vehiculoId);
    }
}