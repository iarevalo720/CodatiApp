using UI.ViewModels.Inicio;
using UI.Views.Clientes;
using UI.Views.Taller;

namespace UI.Views.Inicio;

public partial class Login : ContentPage
{
	private readonly LoginViewModel _loginViewModel;
	public Login(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
		_loginViewModel = loginViewModel;
        SecureStorage.RemoveAll();
	}

    private async void BtnLogin(object sender, EventArgs e)
    {
        await _loginViewModel.Login();
        string? rol = await SecureStorage.GetAsync("rol");

        if (rol == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido obtener el rol del usuario", "OK");
            return;
        }

        switch (rol)
        {
            case "admin":
                await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                break;

            case "mecanico":
                await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                break;

            case "Secretaria":
                await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                break;

            case "Cliente":
                await Shell.Current.GoToAsync($"{nameof(C_menu)}");
                break;
        }
    }

    private void BtnIrRegistro(object sender, EventArgs e)
    {

    }
}