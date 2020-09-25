using CentralLog;
using LoggingDemo.CalculationILogger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingDemo.CalculationLogContext
{
  public class CalculationServiceLogContext : IHostedService
  {
    private readonly ILogger<CalculationServiceLogContext> _logger;
    private readonly IMathOperationsLogContext _mathOperations;
    private readonly ILogContext _logContext;

    public CalculationServiceLogContext(ILogger<CalculationServiceLogContext> logger, IMathOperationsLogContext mathOperations, ILogContext logContext)
    {
      _logger = logger;
      _mathOperations = mathOperations;
       _logContext = logContext;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.Log(LogLevel.Information, "CalculationServiceLogContext StartAsync");
      //why does this not work?
     // LogContext.Context.AddLog(LogLevel.Information, "CalculationServiceLogContext StartAsync");

      var taskList = new List<Task>();
      taskList.Add(Task.Run(() => _mathOperations.Factorial(1)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(2)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(3)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(4)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(5)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(6)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(7)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(8)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(10)));

      await Task.WhenAll(taskList);

      //var logs = LogContext.Context.GetLogs(LogLevel.Information);
      //var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(logs);
      //_logger.Log(LogLevel.Information, jsonResult);

      _logger.Log(LogLevel.Information, "Found All Factorial Results");

      // return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.Log(LogLevel.Information, "CalculationService StopAsync");

      return Task.CompletedTask;
    }
  }
}
