using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CentralLogging
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-3
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      try
      {
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception exception)
      {
        //NLog: catch setup errors
        logger.Error(exception, "Stopped program because of exception");
        throw;
      }
      finally
      {
        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        NLog.LogManager.Shutdown();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
          })
          .ConfigureLogging(logging =>
          {
            logging.ClearProviders(); //remove all logging provider first because by default the framework will add Console,Debug, TraceSource and EventSource providers
            logging.SetMinimumLevel(LogLevel.Debug);
          })
        .UseNLog();  // NLog: Setup NLog for Dependency injection
  }
}
