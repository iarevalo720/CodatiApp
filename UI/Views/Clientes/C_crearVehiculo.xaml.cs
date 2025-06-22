using UI.ViewModels.Clientes;

namespace UI.Views.Clientes;

public partial class C_crearVehiculo : ContentPage
{
	private readonly C_crearVehiculoViewModel _viewModel;
    public C_crearVehiculo(C_crearVehiculoViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void PickerMarcas_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _viewModel.CargarModelosPorMarca();
    }

    private async void BtnRegistrarVehiculo(object sender, EventArgs e)
    {
        await _viewModel.GuardarVehiculo();
    }
}