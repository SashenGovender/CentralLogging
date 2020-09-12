using CentralLog;
using LoggingDemo.CalculationLogContext;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingDemo
{
  public class MathOperationsLogContext : IMathOperationsLogContext
  {
    private readonly ILogger<MathOperationsLogContext> _logger;
    private readonly ILogContext _logContext;

    public MathOperationsLogContext(ILogger<MathOperationsLogContext> logger, ILogContext logContext)
    {
      _logger = logger;
      _logContext = logContext;
    }

    public async Task <double> Factorial(int number)
    {
      LogContext.Context.AddLog(LogLevel.Information,$"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - before calculation");
      double result = 1;

      await Task.Delay(1000);

      LogContext.Context.AddLog(LogLevel.Information, $"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - during calculation ");
      for ( int num=2;num<=number; num++)
      {
        result = result * num;
      }

      await Task.Delay(1000);

      LogContext.Context.AddLog(LogLevel.Information, $"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - after calculation - {result} ");

      var logs = LogContext.Context.GetLogs(LogLevel.Information);
      var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(logs);
      _logger.Log(LogLevel.Information, jsonResult);

      return result;
    }
  }
}
