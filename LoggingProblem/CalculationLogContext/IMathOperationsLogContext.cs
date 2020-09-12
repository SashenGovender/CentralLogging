using System.Threading.Tasks;

namespace LoggingProblem.CalculationLogContext
{
  public interface IMathOperationsLogContext
  {
    Task<double> Factorial(int number);
  }
}