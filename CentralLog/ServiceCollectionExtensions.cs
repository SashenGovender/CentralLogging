using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace CentralLog
{
  public static class ServiceCollectionExtensions
  {
    public static void AddLogContextProivder(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
      //serviceCollection.Configure<LogContextOptions>(options => configuration.GetSection(""));
      serviceCollection.AddSingleton<ILogContext, LogContext>();
    }
  }
}
