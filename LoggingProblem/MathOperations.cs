using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingProblem
{
  public class MathOperations : IMathOperations
  {
    private readonly ILogger<MathOperations> _logger;

    public MathOperations(ILogger<MathOperations> logger)
    {
      _logger = logger;
    }

    public async Task <double> Factorial(int number)
    {
      _logger.LogInformation($"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - before calculation");
      double result = 1;
      await Task.Delay(1000);
      for( int num=2;num<=number; num++)
      {
        result = result * num;
        if(num %2 ==0)
        {
          _logger.LogInformation($"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - during calculation ");
        }
      }
      _logger.LogInformation($"ThreadId: {Thread.CurrentThread.ManagedThreadId} - Number {number} - after calculation - {result} ");
      return result;
    }
  }
}
