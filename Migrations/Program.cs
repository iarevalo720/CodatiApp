using Data; // Espacio de nombres donde está tu AppDbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodatiApp.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString =
                      "Host=dpg-d3gjumogjchc739rdou0-a.oregon-postgres.render.com;"
                    + "Port=5432;"
                    + "Database=codati_database;"
                    + "Username=root;"
                    + "Password=VyWdvbvw08JjO5r6UNWRUDpSmre2OzXk;"
                    + "SSL Mode=Require;"
                    + "Trust Server Certificate=true;";

                    //var connectionString = "Host=localhost;Database=db_codati;Username=postgres;Password=258364";

                    // Registrar el DbContext en el contenedor DI con el TFM compatible.
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));
                });
    }
}
