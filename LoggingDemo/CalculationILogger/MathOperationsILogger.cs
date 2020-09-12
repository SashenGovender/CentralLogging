using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingDemo.CalculationILogger
{
  public class MathOperationsILogger : IMathOperationsILogger
  {
    private readonly ILogger<MathOperationsILogger> _logger;

    public MathOperationsILogger(ILogger<MathOperationsILogger> logger)
    {
      _logger = logger;
    }

    public async Task<double> Factorial(int number)
    {
      _logger.Log(LogLevel.Information, $"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - before calculation");
      double result = 1;

      await Task.Delay(1000);

      _logger.Log(LogLevel.Information, $"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - during calculation ");
      for (int num = 2; num <= number; num++)
      {
        result = result * num;
      }

      await Task.Delay(1000);

      _logger.Log(LogLevel.Information, $"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - after calculation - {result} ");

      return result;
    }
  }
}
