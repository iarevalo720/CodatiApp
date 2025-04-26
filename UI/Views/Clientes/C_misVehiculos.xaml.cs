using UI.ViewModels.Clientes;

namespace UI.Views.Clientes;

public partial class C_misVehiculos : ContentPage
{
    private readonly C_misVehiculosViewModel _viewModel;
	public C_misVehiculos(C_misVehiculosViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void BtnIrCrearVehiculo(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(C_crearVehiculo));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerVehiculos();
    }
}