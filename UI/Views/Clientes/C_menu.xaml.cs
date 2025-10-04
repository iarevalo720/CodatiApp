using UI.ViewModels.Clientes;
using UI.Views.Inicio;

namespace UI.Views.Clientes;

public partial class C_menu : ContentPage
{
    private readonly C_menuViewModel _viewModel;
	public C_menu(C_menuViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        GuardarCredenciales();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.obtenerNombre();
    }

    private async void GuardarCredenciales()
    {
        string? token = await SecureStorage.GetAsync("token");
        string? id = await SecureStorage.GetAsync("id");
        string? nombre = await SecureStorage.GetAsync("nombre");
    }

    private async void BtnCerrarSeccion(object sender, EventArgs e)
    {
        SecureStorage.RemoveAll();

        await Shell.Current.GoToAsync($"//{nameof(Login)}");
    }

    private async void BtnIrVehiculos(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(C_misVehiculos)}");
    }

    private async void BtnIrOrdenes(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(C_crearOrden)}");
    }

    private async void BtnIrAcercaDe(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(C_acercaDe)}");
    }

    private async void BtnIrMisOrdenes(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(C_misOrdenes)}");
    }
}