using GeneralOperationsAPI.Processor;
using NUnit.Framework;

namespace GeneralOperationsAPI.Tests
{
  [TestFixture]
  public class WordCounterTests
  {

    private WordCounter GetSystemUnderTest()
    {
      return new WordCounter();

    }
    [Test]
    public void CountPerLetter_GivenValidWord_ReturnCountsPerCharacter()
    {
      //Arrange
      var wordCounter = GetSystemUnderTest();

      //Act
      var counts =wordCounter.CountPerLetter("Awesome");

      //Assert
      Assert.AreEqual(1, counts['A']);
      Assert.AreEqual(1, counts['w']);
      Assert.AreEqual(2, counts['e']);
    }

    [Test]
    public void CountPerLetter_GivenValidWord_DontReturnCountForNonExistanceCharacters()
    {
      //Arrange
      var wordCounter = GetSystemUnderTest();

      //Act
      var counts = wordCounter.CountPerLetter("Awesome");

      //Assert
      Assert.IsFalse(counts.ContainsKey('z'));
    }
  }
}