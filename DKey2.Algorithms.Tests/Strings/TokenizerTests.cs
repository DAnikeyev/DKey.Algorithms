using DKey.Algorithms.TextProcessing;

namespace DKey.Algorithms.Tests.Strings;

[TestFixture]
public class TokenizerTests
{
    [Test]
    public void T01_Split_TakeOnlyLetters_ReturnsCorrectTokens()
    {
        
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello", "World", "Testing", "tokens" };

        
        var result = Tokenizer.Split(inputText, TokenizerMode.TakeOnlyLetters);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void T02_Split_WhiteSpaces_ReturnsCorrectTokens()
    {
        
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello,", "World!", "123", "Testing", "tokens?" };

        
        var result = Tokenizer.Split(inputText, TokenizerMode.WhiteSpaces);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
    
    [Test]
    public void T03_Split_TakeOnlyLettersOrDigit_ReturnsCorrectTokens()
    {
        
        string inputText = "Hello, World! 123 Testing tokens?";
        var expectedResult = new List<string> { "Hello", "World", "123", "Testing", "tokens" };

        
        var result = Tokenizer.Split(inputText, TokenizerMode.TakeOnlyLettersOrDigit);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }
}