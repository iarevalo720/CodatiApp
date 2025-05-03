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
                    // Configurar cadena de conexión. Puedes obtenerla del archivo de configuración.
                    //var connectionString = "postgresql://root:mdfuf1KnBFTrCRaKaQpG3oLRb5YK4TX3@dpg-d079j3s9c44c739qa7hg-a.oregon-postgres.render.com/test_database_xmim";

                    var connectionString =
                      "Host=dpg-d079j3s9c44c739qa7hg-a.oregon-postgres.render.com;"
                    + "Port=5432;"
                    + "Database=test_database_xmim;"
                    + "Username=root;"
                    + "Password=mdfuf1KnBFTrCRaKaQpG3oLRb5YK4TX3;"
                    + "SSL Mode=Require;"
                    + "Trust Server Certificate=true;";

                    // Registrar el DbContext en el contenedor DI con el TFM compatible.
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                });
    }
}
