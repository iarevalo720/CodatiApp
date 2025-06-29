using System.Threading.Tasks;
using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_timbrado : ContentPage
{
	private readonly T_timbradoViewModel _viewModel;
    public T_timbrado(T_timbradoViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerTimbrados();
    }

    private async void BtnRegistrarTimbrado(object sender, EventArgs e)
    {
        await _viewModel.RegistrarTimbrado();
    }

    private async void BtnSeleccionarTimbrado(object sender, EventArgs e)
    {
        if (await DisplayAlert("Seleccionar timbrado", "Esta seguro de seleccionar este timbrado para generar los comprobantes?", "Si", "No"))
        {
            int timbradoId = ObtenerTimbradoId((Button)sender);
            await _viewModel.SeleccionarTimbrado(timbradoId);
        }
    }

    private async void BtnCambiarEstadoTimbrado(object sender, EventArgs e)
    {
        if (await DisplayAlert("Cambiar estado", "Esta seguro de cambiar el estado del timbrado?", "Si", "No"))
        {
            int timbradoId = ObtenerTimbradoId((Button)sender);
            await _viewModel.CambiarEstadoTimbrado(timbradoId);
        }
    }

    private int ObtenerTimbradoId(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)((VerticalStackLayout)grid.Children.FirstOrDefault()).Children.FirstOrDefault()).Text.Substring(1));
    }
}