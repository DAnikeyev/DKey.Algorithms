using DKey.Algorithms.TextProcessing;

namespace DKey.Algorithms.Tests;

[TestFixture]
public class TokenizerTests
{
    [Test]
    public void Split_TakeOnlyLetters_ReturnsCorrectTokens()
    {
        // Arrange
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello", "World", "Testing", "tokens" };

        // Act
        var result = Tokenizer.Split(inputText, TokenizerMode.TakeOnlyLetters);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void Split_WhiteSpaces_ReturnsCorrectTokens()
    {
        // Arrange
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello,", "World!", "123", "Testing", "tokens?" };

        // Act
        var result = Tokenizer.Split(inputText, TokenizerMode.WhiteSpaces);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
    
    [Test]
    public void Split_TakeOnlyLettersOrDigit_ReturnsCorrectTokens()
    {
        // Arrange
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello", "World", "123", "Testing", "tokens" };

        // Act
        var result = Tokenizer.Split(inputText, TokenizerMode.TakeOnlyLettersOrDigit);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}