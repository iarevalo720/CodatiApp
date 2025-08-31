using UI.ViewModels.Taller;

namespace UI.Views.Taller;

public partial class T_informe : ContentPage
{
	private readonly T_informeViewModel _viewModel;
	public T_informe(T_informeViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}