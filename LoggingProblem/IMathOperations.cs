using System.Threading.Tasks;

namespace LoggingProblem
{
  public interface IMathOperations
  {
     Task<double> Factorial(int number);
  }
}