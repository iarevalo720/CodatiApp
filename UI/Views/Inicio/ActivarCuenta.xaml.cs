using UI.ViewModels.Inicio;

namespace UI.Views.Inicio;

public partial class ActivarCuenta : ContentPage
{
	private readonly ActivarCuentaViewModel _viewModel;
    public ActivarCuenta(ActivarCuentaViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void BtnActivarCuenta(object sender, EventArgs e)
    {
        await _viewModel.ActivarCuenta();
    }
}