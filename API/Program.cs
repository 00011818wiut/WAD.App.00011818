using Microsoft.AspNetCore.Hosting;

using API;

CreateHostBuilder().Build().Run();

static IHostBuilder CreateHostBuilder() =>
          Host.CreateDefaultBuilder()
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              });