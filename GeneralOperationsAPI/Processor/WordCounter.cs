using CentralLog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace GeneralOperationsAPI.Processor
{
  public class WordCounter : IWordCounter
  {
    public Dictionary<char, int> CountPerLetter(string word)
    {
      Console.WriteLine($"CountPerLetter - ThreadId - {Thread.CurrentThread.ManagedThreadId} ");
      Regex pattern = new Regex("[^a-zA-z0-9]+");
      string alphanumericCharacters = pattern.Replace(word, "");

      var characterCountDictionary = new Dictionary<char, int>();
      int count = 0;
      foreach (char character in alphanumericCharacters)
      {
        if (characterCountDictionary.ContainsKey(character))
        {
          characterCountDictionary[character]++;
        }
        else
        {
          LogContext.Context.AddLog(LogLevel.Debug, $"Count: {count} -  'Added new character '{character}' to the dictionary", 0);
          characterCountDictionary.Add(character, 1);
          count++
          ;
        }
      }

      return characterCountDictionary;
    }
  }
}
