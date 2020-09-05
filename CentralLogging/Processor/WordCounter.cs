using CentralLogging.Observability;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLogging.Processor
{
  public class WordCounter : IWordCounter
  {
    public Dictionary<char, int> CountPerLetter(string word)
    {
      Console.WriteLine($"CountPerLetter - ThreadId - {Thread.CurrentThread.ManagedThreadId} ");

      var characterCountDictionary = new Dictionary<char, int>();
      int count = 0;
      foreach (char character in word)
      {
        if (characterCountDictionary.ContainsKey(character))
        {
          characterCountDictionary[character]++;
        }
        else
        {
          LogContext.Context.AddLog(LogLevel.Debug, $"Count: {count} -  'Added new character '{character}' to the dictionary");
          characterCountDictionary.Add(character, 0);
          count++;
        }
      }

      return characterCountDictionary;
    }
  }
}
