using Data; // Espacio de nombres donde está tu AppDbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CodatiApp.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configuración del host (esto es útil para utilizar las herramientas de EF Core)
            var host = CreateHostBuilder(args).Build();
            // Aquí podrías ejecutar comandos o simplemente dejarlo preparado para EF
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Agregamos la configuración, por ejemplo, usando appsettings.json.
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Configurar cadena de conexión. Puedes obtenerla del archivo de configuración.
                    var connectionString = "Host=localhost;Database=db_taller;Username=postgres;Password=258364";

                    // Registrar el DbContext en el contenedor DI con el TFM compatible.
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                    // Si tienes otros servicios/referencias necesarias para migraciones, regístralos aquí.

                    // (Opcional) Si quieres que EF Core escanee las migraciones en un assembly específico:
                    // services.AddDbContext<AppDbContext>(options =>
                    //    options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));
                });
    }
}
