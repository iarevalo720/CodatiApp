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
using Microsoft.AspNetCore.DataProtection;

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
                //var connectionString = "Host=localhost;Database=db_codati;Username=postgres;Password=258364";
                var connectionString = "Host=dpg-d3gjumogjchc739rdou0-a.oregon-postgres.render.com;Database=codati_database;Username=root;Password=VyWdvbvw08JjO5r6UNWRUDpSmre2OzXk";

                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("Migrations");
                });
            });

            string keysDirectory = Path.Combine(FileSystem.AppDataDirectory, "dataprotection_keys");
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysDirectory))
                .SetApplicationName("CodatiApp");

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            //Views
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<ActivarCuenta>();
            builder.Services.AddTransient<Informaciones>();
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
            builder.Services.AddTransient<T_modelos>();
            builder.Services.AddTransient<T_timbrado>();
            builder.Services.AddTransient<T_funcionarios>();
            builder.Services.AddTransient<T_informe>();

            builder.Services.AddTransient<C_menu>();
            builder.Services.AddTransient<C_acercaDe>();
            builder.Services.AddTransient<C_misVehiculos>();
            builder.Services.AddTransient<C_crearVehiculo>();
            builder.Services.AddTransient<C_crearOrden>();
            builder.Services.AddTransient<C_misOrdenes>();

            //ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ActivarCuentaViewModel>();
            builder.Services.AddTransient<InformacionesViewModel>();
            builder.Services.AddTransient<T_menuViewModel>();
            builder.Services.AddTransient<T_ordenesViewModel>();
            builder.Services.AddTransient<T_ordenDetalleViewModel>();
            builder.Services.AddTransient<T_gestionOrdenDetalleViewModel>();
            builder.Services.AddTransient<T_clienteViewModel>();
            builder.Services.AddTransient<C_menuViewModel>();
            builder.Services.AddTransient<C_misVehiculosViewModel>();
            builder.Services.AddTransient<C_crearVehiculoViewModel>();
            builder.Services.AddTransient<C_crearOrdenViewModel>();
            builder.Services.AddTransient<C_misOrdenesViewModel>();
            builder.Services.AddTransient<T_vehiculosViewModel>();
            builder.Services.AddTransient<T_modificarVehiculoVIewModel>();
            builder.Services.AddTransient<T_categoriasViewModel>();
            builder.Services.AddTransient<T_serviciosViewModel>();
            builder.Services.AddTransient<T_marcasViewModel>();
            builder.Services.AddTransient<T_modelosViewModel>();
            builder.Services.AddTransient<T_timbradoViewModel>();
            builder.Services.AddTransient<T_funcionariosViewModel>();
            builder.Services.AddTransient<T_informeViewModel>();

            //Services & Repositories
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IVehiculoService, VehiculoService>();
            builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            builder.Services.AddScoped<IEmailService, EmailService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
