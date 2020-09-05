using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingProblem
{
  public class CalculationService : IHostedService
  {
    private readonly ILogger<CalculationService> _logger;
    private readonly IMathOperations _mathOperations;

    public CalculationService(ILogger<CalculationService> logger, IMathOperations mathOperations)
    {
      _logger = logger;
      _mathOperations = mathOperations;
    }

    public  Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.Log(LogLevel.Information, "CalculationService StartAsync");

      var taskList = new List<Task>();
      taskList.Add(Task.Run( async () => _mathOperations.Factorial(10)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(600)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(70)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(80)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(90)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(100)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(110)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(120)));
      taskList.Add(Task.Run(() => _mathOperations.Factorial(130)));

       Task.WhenAll(taskList);
      _logger.Log(LogLevel.Information, "Found All Factorial Results");
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.Log(LogLevel.Information, "CalculationService StopAsync");

      return Task.CompletedTask;
    }
  }
}
