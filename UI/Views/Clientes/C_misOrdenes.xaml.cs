using UI.ViewModels.Clientes;

namespace UI.Views.Clientes;

public partial class C_misOrdenes : ContentPage
{
    private readonly C_misOrdenesViewModel _viewModel;
    
    public C_misOrdenes(C_misOrdenesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarMisOrdenes();
    }
}