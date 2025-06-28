using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_funcionarios : ContentPage
{
	private readonly T_funcionariosViewModel _viewModel;
	public T_funcionarios(T_funcionariosViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

    private async void BtnBuscarFuncionario(object sender, EventArgs e)
    {
        await _viewModel.ObtenerFuncionario();
    }

    private void BtnLimpiarCampos(object sender, EventArgs e)
    {
        _viewModel.LimpiarCampos();
    }

    private async void BtnGuardarCambiosUsuario(object sender, EventArgs e)
    {
        if (await DisplayAlert("Guardar cambios", "Esta seguro de actualizar los datos del funcionario?", "Si", "No"))
        {
            await _viewModel.GuardarCambiosUsuario();
        }
    }

    private async void BtnCambiarEstadoFuncionario(object sender, EventArgs e)
    {
        await _viewModel.CambiarEstadoFuncionario();
    }

    private async void BtnCrearFuncionario(object sender, EventArgs e)
    {
        await _viewModel.CrearFuncionario();
    }

    private async void BtnRestablecerContrasena(object sender, EventArgs e)
    {
        await _viewModel.RestablecerContrasena();
    }
}