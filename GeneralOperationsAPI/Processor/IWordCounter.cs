using System.Collections.Generic;

namespace GeneralOperationsAPI.Processor
{
  public interface IWordCounter
  {
    Dictionary<char, int> CountPerLetter(string word);
  }
}