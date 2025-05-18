using Data;
using Core.Entities;
using Core.Interfaces;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Services;
using UI.ViewModels.Inicio;
using UI.ViewModels.Taller;
using UI.Views.Inicio;
using UI.Views.Taller;
using UI.Views.Clientes;
using UI.ViewModels.Clientes;

namespace UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("fontello.ttf", "Icons");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                //var connectionString = "Host=localhost;Database=db_taller;Username=postgres;Password=258364";
                var connectionString =
                  "Host=dpg-d079j3s9c44c739qa7hg-a.oregon-postgres.render.com;"
                + "Port=5432;"
                + "Database=test_database_xmim;"
                + "Username=root;"
                + "Password=mdfuf1KnBFTrCRaKaQpG3oLRb5YK4TX3;"
                + "SSL Mode=Require;"
                + "Trust Server Certificate=true;";

                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("Migrations");
                });
            });

            //Add identity
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddSignInManager()
            //    .AddRoles<IdentityRole>();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>();

            //Views
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<T_menu>();
            builder.Services.AddTransient<T_ordenes>();
            builder.Services.AddTransient<T_ordenDetalle>();
            builder.Services.AddTransient<T_gestionOrdenDetalle>();
            builder.Services.AddTransient<T_cliente>();
            builder.Services.AddTransient<T_vehiculos>();
            builder.Services.AddTransient<T_modificarVehiculo>();
            builder.Services.AddTransient<T_categorias>();
            builder.Services.AddTransient<T_servicios>();
            builder.Services.AddTransient<T_marcas>();

            builder.Services.AddTransient<C_menu>();
            builder.Services.AddTransient<C_acercaDe>();
            builder.Services.AddTransient<C_misVehiculos>();
            builder.Services.AddTransient<C_crearVehiculo>();
            builder.Services.AddTransient<C_crearOrden>();

            //ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<T_menuViewModel>();
            builder.Services.AddTransient<T_ordenesViewModel>();
            builder.Services.AddTransient<T_ordenDetalleViewModel>();
            builder.Services.AddTransient<T_gestionOrdenDetalleViewModel>();
            builder.Services.AddTransient<T_clienteViewModel>();
            builder.Services.AddTransient<C_menuViewModel>();
            builder.Services.AddTransient<C_misVehiculosViewModel>();
            builder.Services.AddTransient<C_crearVehiculoViewModel>();
            builder.Services.AddTransient<C_crearOrdenViewModel>();
            builder.Services.AddTransient<T_vehiculosViewModel>();
            builder.Services.AddTransient<T_modificarVehiculoVIewModel>();
            builder.Services.AddTransient<T_categoriasViewModel>();
            builder.Services.AddTransient<T_serviciosViewModel>();
            builder.Services.AddTransient<T_marcasViewModel>();

            //Services & Repositories
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IVehiculoService, VehiculoService>();
            builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
