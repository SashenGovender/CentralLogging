using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralLogging.Processor
{
  public class WordCounter : IWordCounter
  {
    public Dictionary<char, int> CountPerLetter(string word)
    {
      var characterCountDictionary = new Dictionary<char, int>();
      foreach (char letter in word)
      {
        if (characterCountDictionary.ContainsKey(letter))
        {
          characterCountDictionary[letter]++;
        }
        else
        {
          characterCountDictionary.Add(letter, 0);
        }
      }

      return characterCountDictionary;
    }
  }
}
