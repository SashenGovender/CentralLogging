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

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.Log(LogLevel.Information, "CalculationService StartAsync");

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
