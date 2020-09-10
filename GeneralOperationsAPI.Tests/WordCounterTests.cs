using CentralLog;
using GeneralOperationsAPI.Processor;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace GeneralOperationsAPI.Tests
{
  [TestFixture]
  public class WordCounterTests
  {
    private struct Stubs
    {
      public ILogContext LogContext { get; set; }
    }

    private static Stubs GetStubs()
    {
      var stubs = new Stubs
      {
        LogContext = Substitute.For<ILogContext>()
      };

      if(LogContext.Context ==null)
      {
        var a =LogContext.Context;
      }
      return stubs;
    }
    private static WordCounter GetSystemUnderTest(Stubs stubs)
    {
      return new WordCounter(stubs.LogContext);

    }
    [Test]
    public void CountPerLetter_GivenValidWord_ReturnCountsPerCharacter()
    {
      //Arrange
      var stubs = GetStubs();
      var wordCounter = GetSystemUnderTest(stubs);

      //Act
      var counts = wordCounter.CountPerLetter("Hello");

      //Assert
      Assert.AreEqual(1, counts['H']);
      Assert.AreEqual(1, counts['e']);
      Assert.AreEqual(2, counts['l']);
      Assert.AreEqual(1, counts['o']);
      stubs.LogContext.Received(4).AddLog(Arg.Any<LogLevel>(), Arg.Any<string>(), Arg.Any<int>());
    }

    [Test]
    public void CountPerLetter_GivenValidWord_DontReturnCountForNonExistanceCharacters()
    {
      //Arrange
      var stubs = GetStubs();
      var wordCounter = GetSystemUnderTest(stubs);

      //Act
      var counts = wordCounter.CountPerLetter("World");

      //Assert
      Assert.IsFalse(counts.ContainsKey('z'));
      stubs.LogContext.Received(5).AddLog(Arg.Any<LogLevel>(), Arg.Any<string>(), Arg.Any<int>());
    }
  }
}