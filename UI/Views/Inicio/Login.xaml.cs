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
        try
        {
            await _loginViewModel.Login();
            string? rol = await SecureStorage.GetAsync("rol");

            switch (rol)
            {
                case "Admin":
                    await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                    break;

                case "Mecanico":
                    await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                    break;

                case "Secretario":
                    await Shell.Current.GoToAsync($"{nameof(T_menu)}");
                    break;

                case "Cliente":
                    await Shell.Current.GoToAsync($"{nameof(C_menu)}");
                    break;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "usuario_inhabilitado")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario está inhabilitado, por favor contactese con el taller", "OK");
            }
            else if (ex.Message == "usuario_no_activo")
            {
                await Application.Current.MainPage.DisplayAlert("Informacion", "La cuenta no está activa, por favor active su cuenta", "OK");
            }
            else if (ex.Message == "campos_vacios")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/contraseña vacia", "OK");
            }
            else if (ex.Message == "credenciales_invalidos" || ex.Message == "usuario_no_encontrado")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email/contraseña invalidos", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Ha ocurrido un error, por favor intentelo más tarde: {ex.Message}", "OK");
                Console.WriteLine($"Error {ex.Message}");
            }
        }
    }

    private async void BtnActivarCuenta(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ActivarCuenta)}");
    }
}