using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_servicios : ContentPage
{
	private readonly T_serviciosViewModel _viewModel;
	public T_servicios(T_serviciosViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}