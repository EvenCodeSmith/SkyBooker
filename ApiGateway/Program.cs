using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ocelot.json einbinden
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Ocelot aktivieren
            builder.Services.AddOcelot();

            var app = builder.Build();

            // Ocelot starten
            await app.UseOcelot();

            app.Run();
        }
    }
}
