using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(vehiculoId), nameof(vehiculoId))]
public partial class T_modificarVehiculo : ContentPage
{
	private readonly T_modificarVehiculoVIewModel _viewModel;
	public int vehiculoId { get; set; }
    public T_modificarVehiculo(T_modificarVehiculoVIewModel vIewModel)
	{
		InitializeComponent();
		_viewModel = vIewModel;
		BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenervehiculoPorId(vehiculoId);
    }

    private async void PickerMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            await _viewModel.CargarModelosPorMarca();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Ocurrió un error al cargar los modelos: {ex.Message}", "OK");
            throw;
        }
    }

    private async void BtnGuardarVehiculoClicked(object sender, EventArgs e)
    {
        await _viewModel.ModificarVehiculo();
    }
}