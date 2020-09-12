using System.Threading.Tasks;

namespace LoggingDemo.CalculationILogger
{
  public interface IMathOperationsILogger
  {
    Task<double> Factorial(int number);
  }
}