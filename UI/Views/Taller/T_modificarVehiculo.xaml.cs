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
    //private void BtnLimpiarCampos(object sender, EventArgs e)
    //{
    //    _viewModel.LimpiarCampos();
    //}
    //private async void BtnModificarVehiculo(object sender, EventArgs e)
    //{
    //    await _viewModel.ModificarVehiculo();
    //    await Shell.Current.GoToAsync($"//{nameof(T_vehiculos)}");
}