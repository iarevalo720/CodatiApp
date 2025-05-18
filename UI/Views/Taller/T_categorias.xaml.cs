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

    private async void BtnIrServicios(object sender, EventArgs e)
    {
        int categoriaId = ObtenerCategoriaId((Button)sender);
        var ruta = $"{nameof(T_servicios)}?categoriaId={categoriaId}";
        await Shell.Current.GoToAsync(ruta);
    }

    private int ObtenerCategoriaId(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)((VerticalStackLayout)grid.Children.FirstOrDefault()).Children.FirstOrDefault()).Text.Substring(1));
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