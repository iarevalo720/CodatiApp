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

    private void BtnSeleccionarTimbrado(object sender, EventArgs e)
    {

    }

    private void BtnCambiarEstadoTimbrado(object sender, EventArgs e)
    {

    }
}