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
}