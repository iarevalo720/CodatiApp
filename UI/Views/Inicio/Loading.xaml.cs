using Core.Interfaces;
using UI.Views.Clientes;
using UI.Views.Taller;

namespace UI.Views.Inicio;

public partial class Loading : ContentPage
{
	private readonly IUserService _userService;

	public Loading(IUserService userService)
	{
		InitializeComponent();
		_userService = userService;
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        string? jwt = await SecureStorage.GetAsync("jwt");

        if (jwt == null)
        {
            await FormatearInicio();
            return;
        }

        bool estaAutenticado = _userService.EstaAutenticado(jwt);
        if (!estaAutenticado) await FormatearInicio();

        string? rol = await SecureStorage.GetAsync("rol");

        if (rol == null)
        {
            await FormatearInicio();
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

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }

    private async Task FormatearInicio()
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync($"{nameof(Login)}");
    }
}