using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace LoggingProblem
{
  public class Program
  {

    static async Task Main(string[] args)
    {
      // https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-3
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      try
      {
        await CreateHostBuilder(args).Build().RunAsync();
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
      await CreateHostBuilder(args).Build().RunAsync();
    }
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder()
          .ConfigureServices(ConfigureServices)
          .ConfigureLogging(logging =>
          {
            logging.ClearProviders(); //remove all logging provider first because by default the framework will add Console,Debug, TraceSource and EventSource providers
            logging.SetMinimumLevel(LogLevel.Debug);
          })
        .UseNLog();  // NLog: Setup NLog for Dependency injection;
    }

    private static void ConfigureServices(HostBuilderContext builder, IServiceCollection serviceCollection)
    {
      serviceCollection.AddHostedService<CalculationService>();
      serviceCollection.AddTransient<IMathOperations, MathOperations>();
    }

  }
}
