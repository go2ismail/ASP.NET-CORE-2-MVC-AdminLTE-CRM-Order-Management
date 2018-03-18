using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using motekarteknologi.Data;
using motekarteknologi.Motekar;

namespace motekarteknologi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    context.Database.EnsureCreated();

                    // Look for any companies.
                    if (!context.Company.Any())
                    {
                        Seed.CreateDefaultCompany(context).Wait();
                        Seed.CreateRoles(serviceProvider).Wait();
                        Seed.CreateUsers(serviceProvider).Wait();
                        Seed.CreateCompanyRole(context).Wait();
                    }

                    

                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An error occurred while creating roles");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
