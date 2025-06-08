using System.Threading.Tasks;
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

    private async void BtnBuscarCliente(object sender, EventArgs e)
    {
		await _viewModel.ObtenerUsuario(_viewModel.TxtCI);
    }

    private void BtnLimpiarCampos(object sender, EventArgs e)
    {
        _viewModel.LimpiarCampos();
    }

    private async void BtnGuardarCambiosUsuario(object sender, EventArgs e)
    {
        await _viewModel.GuardarCambiosUsuario();
    }

    private async void BtnCambiarEstadoCliente(object sender, EventArgs e)
    {
        await _viewModel.CambiarEstadoCliente();
    }

    private async void BtnCrearCliente(object sender, EventArgs e)
    {
        await _viewModel.CrearCliente();
    }

    private async void BtnRestablecerContrasena(object sender, EventArgs e)
    {
        await _viewModel.RestablecerContrasena();
    }
}