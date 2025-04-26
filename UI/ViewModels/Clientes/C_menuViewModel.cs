using PropertyChanged;

namespace UI.ViewModels.Clientes
{
    [AddINotifyPropertyChangedInterface]
    public class C_menuViewModel
    {
        public string NombreUsuario { get; set; }

        public C_menuViewModel()
        {
            
        }

        public async Task obtenerNombre()
        {
            string nombre = await SecureStorage.GetAsync("nombre");
            NombreUsuario = nombre;
        }
    }
}

