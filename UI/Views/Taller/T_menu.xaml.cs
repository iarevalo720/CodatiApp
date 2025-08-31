using System.Threading.Tasks;
using UI.ViewModels.Taller;
using UI.Views.Inicio;

namespace UI.Views.Taller;

public partial class T_menu : ContentPage
{
    private readonly T_menuViewModel _t_MenuViewModel;
    public T_menu(T_menuViewModel t_MenuViewModel)
    {
        InitializeComponent();
        _t_MenuViewModel = t_MenuViewModel;
        BindingContext = _t_MenuViewModel;
    }

    private async void BtnCerrarSeccion(object sender, EventArgs e)
    {
        await CerrarSeccion();
    }

    private async void BtnIrOrdenes(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_ordenes)}");
    }

    private async Task CerrarSeccion()
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync($"//{nameof(Login)}");
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }

    private async void BtnIrClientes(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_cliente)}");
    }

    private async void BtnIrVehiculos(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_vehiculos)}");
    }

    private async void BtnIrCategorias(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_categorias)}");
    }

    private async void BtnIrMarcas(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_marcas)}");
    }

    private async void BtnIrFuncionarios(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_funcionarios)}");
    }

    private async void BtnIrTimbrados(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_timbrado)}");
    }

    private async void BtnIrInformes(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(T_informe)}");
    }
}
