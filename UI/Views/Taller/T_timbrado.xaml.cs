using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_timbrado : ContentPage
{
	private readonly T_timbradoViewModel _viewModel;
    public T_timbrado(T_timbradoViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }
}