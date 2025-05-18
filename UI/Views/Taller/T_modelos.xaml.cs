using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(marcaId), nameof(marcaId))]
public partial class T_modelos : ContentPage
{
	private readonly T_modelosViewModel _viewModel;
    public int marcaId { get; set; }
    public T_modelos(T_modelosViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerModelosPorMarcaId(marcaId);
    }

    private async void BtnCambiarNombreModelo(object sender, EventArgs e)
    {
        int modeloId = ObtenerModeloId((Button)sender);
        await _viewModel.CambiarNombreModelo(modeloId);
    }
    private async void BtnCambiarEstadoModelo(object sender, EventArgs e)
    {
        if (await DisplayAlert("Cambiar estado", "Esta seguro de cambiar el estado del modelo del vehiculo?", "Si", "No"))
        {
            int modeloId = ObtenerModeloId((Button)sender);
            await _viewModel.CambiarEstadoModelo(modeloId);
        }
    }
    private async void BtnAgregarModelo(object sender, EventArgs e)
    {
        if (await DisplayAlert("Registrar modelo", "Esta seguro de registrar un nuevo modelo de vehiculo?", "Si", "No"))
        {
            await _viewModel.CrearNuevoModelo(marcaId);
        }
    }

    private int ObtenerModeloId(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent.Parent;
        return int.Parse(((Label)((VerticalStackLayout)grid.Children.FirstOrDefault()).Children.FirstOrDefault()).Text.Substring(1));
    }
}