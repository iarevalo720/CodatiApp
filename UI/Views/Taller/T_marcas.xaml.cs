using System.Threading.Tasks;
using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_marcas : ContentPage
{
	private readonly T_marcasViewModel _viewModel;
    public T_marcas(T_marcasViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerTodasMarcas();
    }

    private async void BtnIrModelos(object sender, EventArgs e)
    {
        string route = $"{nameof(T_modelos)}?marcaId={ObtenerMarcaId((Button)sender)}";
        await Shell.Current.GoToAsync(route);
    }

    private async void BtnCambiarNombreMarca(object sender, EventArgs e)
    {
        int marcaId = ObtenerMarcaId((Button)sender);
        await _viewModel.CambiarNombreMarca(marcaId);
    }

    private async void BtnCambiarEstadoMarca(object sender, EventArgs e)
    {
        if (await DisplayAlert("Cambiar estado", "Esta seguro de cambiar el estado de la marca del vehiculo?", "Si", "No"))
        {
            int marcaId = ObtenerMarcaId((Button)sender);
            await _viewModel.CambiarEstadoMarca(marcaId);
        }
    }

    private async void BtnAgregarMarca(object sender, EventArgs e)
    {
        if (await DisplayAlert("Registrar marca", "Esta seguro de registrar una nueva marca de vehiculo?", "Si", "No"))
        {
            await _viewModel.CrearNuevaMarca();
        }
    }
    private int ObtenerMarcaId(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)((VerticalStackLayout)grid.Children.FirstOrDefault()).Children.FirstOrDefault()).Text.Substring(1));
    }
}