using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_categorias : ContentPage
{
	private readonly T_categoriasViewModel _viewModel;
	public T_categorias(T_categoriasViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerCategorias();
    }

    private void BtnIrDetalles(object sender, EventArgs e)
    {

    }

    private int ObtenerCategoriaId(object sender)
    {
        VerticalStackLayout verticalStackLayout = (VerticalStackLayout)((Button)sender).Parent.Parent;
        return int.Parse(((Label)verticalStackLayout.Children.FirstOrDefault()).Text.Substring(1));
    }

    private async void BtnCambiarNombreCategoria(object sender, EventArgs e)
    {
        int categoriaId = ObtenerCategoriaId((Button)sender);
        await _viewModel.CambiarNombreCategoria(categoriaId);
    }

    private async void BtnCambiarEstadoCategoria(object sender, EventArgs e)
    {
        if (await DisplayAlert("Cambiar estado", "Esta seguro de cambiar el estado de la categoria?", "Si", "No"))
        {
            int categoriaId = ObtenerCategoriaId((Button)sender);
            await _viewModel.CambiarEstadoCategoria(categoriaId);
        }
    }
}