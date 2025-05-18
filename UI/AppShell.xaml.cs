using UI.Views.Clientes;
using UI.Views.Taller;

namespace UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(T_menu), typeof(T_menu));
            Routing.RegisterRoute(nameof(T_ordenes), typeof(T_ordenes));
            Routing.RegisterRoute(nameof(T_ordenDetalle), typeof(T_ordenDetalle));
            Routing.RegisterRoute(nameof(T_gestionOrdenDetalle), typeof(T_gestionOrdenDetalle));
            Routing.RegisterRoute(nameof(T_cliente), typeof(T_cliente));
            Routing.RegisterRoute(nameof(T_vehiculos), typeof(T_vehiculos));
            Routing.RegisterRoute(nameof(T_modificarVehiculo), typeof(T_modificarVehiculo));
            Routing.RegisterRoute(nameof(T_categorias), typeof(T_categorias));
            Routing.RegisterRoute(nameof(T_servicios), typeof(T_servicios));
            Routing.RegisterRoute(nameof(T_marcas), typeof(T_marcas));
            Routing.RegisterRoute(nameof(T_modelos), typeof(T_modelos));

            Routing.RegisterRoute(nameof(C_menu), typeof(C_menu));
            Routing.RegisterRoute(nameof(C_acercaDe), typeof(C_acercaDe));
            Routing.RegisterRoute(nameof(C_misVehiculos), typeof(C_misVehiculos));
            Routing.RegisterRoute(nameof(C_crearVehiculo), typeof(C_crearVehiculo));
            Routing.RegisterRoute(nameof(C_crearOrden), typeof(C_crearOrden));
        }
    }
}