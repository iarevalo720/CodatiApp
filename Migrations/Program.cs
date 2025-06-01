using Data; // Espacio de nombres donde está tu AppDbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

                    var connectionString =
                      "Host=dpg-d0p4c58dl3ps73afq3r0-a.oregon-postgres.render.com;"
                    + "Port=5432;"
                    + "Database=test_jwgf;"
                    + "Username=root;"
                    + "Password=VMZitaHtWnrBrk9jDtwQm7kLnda4voSB;"
                    + "SSL Mode=Require;"
                    + "Trust Server Certificate=true;";

                    // Registrar el DbContext en el contenedor DI con el TFM compatible.
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                });
    }
}
