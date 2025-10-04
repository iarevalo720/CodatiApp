using UI.ViewModels.Inicio;

namespace UI.Views.Inicio;

public partial class Informaciones : ContentPage
{
    private readonly InformacionesViewModel _viewModel;

    public Informaciones(InformacionesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarInformaciones();
    }
}