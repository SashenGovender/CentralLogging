using System.Threading.Tasks;

namespace LoggingProblem.CalculationILogger
{
  public interface IMathOperationsILogger
  {
    Task<double> Factorial(int number);
  }
}