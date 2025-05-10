using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_cliente : ContentPage
{
	private readonly T_clienteViewModel _viewModel;
	public T_cliente(T_clienteViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    private void BtnBuscarCliente(object sender, EventArgs e)
    {
		_viewModel.ObtenerUsuario(_viewModel.TxtCI);
    }

    private void BtnLimpiarCampos(object sender, EventArgs e)
    {
        _viewModel.LimpiarCampos();
    }

    private async void BtnGuardarCambiosUsuario(object sender, EventArgs e)
    {
        await _viewModel.GuardarCambiosUsuario();
    }
}