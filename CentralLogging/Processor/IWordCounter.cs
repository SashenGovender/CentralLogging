using System.Collections.Generic;

namespace CentralLogging.Processor
{
  public interface IWordCounter
  {
    Dictionary<char, int> CountPerLetter(string word);
  }
}