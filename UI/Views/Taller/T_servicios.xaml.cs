using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(categoriaId), nameof(categoriaId))]
public partial class T_servicios : ContentPage
{
	private readonly T_serviciosViewModel _viewModel;
	public int categoriaId { get; set; }
	public T_servicios(T_serviciosViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.ObtenerServiciosPorCategoriaId(categoriaId);
    }

    private async void BtnCambiarNombreServicio(object sender, EventArgs e)
    {
        int servicioId = ObtenerServicioId((Button)sender);
        await _viewModel.CambiarNombreServicio(servicioId);
    }
    private async void BtnCambiarEstadoServicio(object sender, EventArgs e)
    {
        if (await DisplayAlert("Cambiar estado", "Esta seguro de cambiar el estado del servicio?", "Si", "No"))
        {
            int servicioId = ObtenerServicioId((Button)sender);
            await _viewModel.CambiarEstadoServicio(servicioId);
        }
    }

    private async void BtnAgregarServicio(object sender, EventArgs e)
    {
        if (await DisplayAlert("Crear servicio", "Esta seguro de crear un servicio?", "Si", "No"))
        {
            await _viewModel.CrearNuevaSubCategoria(categoriaId);
        }
    }

    private int ObtenerServicioId(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)((VerticalStackLayout)grid.Children.FirstOrDefault()).Children.FirstOrDefault()).Text.Substring(1));
    }
}