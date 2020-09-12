using System.Threading.Tasks;

namespace LoggingDemo.CalculationLogContext
{
  public interface IMathOperationsLogContext
  {
    Task<double> Factorial(int number);
  }
}