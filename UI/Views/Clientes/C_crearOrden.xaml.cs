using UI.ViewModels.Clientes;

namespace UI.Views.Clientes;

public partial class C_crearOrden : ContentPage
{
    private readonly C_crearOrdenViewModel _viewModel;
    public C_crearOrden(C_crearOrdenViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarVehiculos();
    }

    private async void PickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        await _viewModel.CargarSubCategorias();
    }

    private void PickerSubCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        _viewModel.CargarListaSubCategorias();
    }

    private async void BtnEliminarSubCategoriaCommand(object sender, EventArgs e)
    {
        var element = (Grid)sender;
        var subcategoria = ((Label)element.Children.LastOrDefault()).Text;
        _viewModel.EliminarSubcategoria(subcategoria);
    }
}