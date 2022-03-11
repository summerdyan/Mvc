// Program.cs does the following on startup:
// Get a database context instance from the dependency injection container.
// Call the DbInitializer.Initialize method.
// Dispose the context when the Initialize method completes.
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mvc.Data;
using System;

namespace Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create host 
            var host = CreateHostBuilder(args).Build();

            // create databases if they don't exist
            CreateDbIfNotExists(host);

            // run host
            host.Run();
        }

        // create the databases if they don't exist
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MvcMovieContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        // create host builder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
