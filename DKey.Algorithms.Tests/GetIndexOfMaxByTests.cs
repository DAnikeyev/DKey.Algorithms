using DKey.Algorithms.IEnumerableExtensions;

namespace DKey.Algorithms.Tests;

public class GetIndexOfMaxByTests
{
    [Test]
    public void GetIndexOfMaxBy_TestWithIntegers_ReturnsCorrectIndex()
    {
        // Arrange
        IEnumerable<int> input = new List<int> { 1, 3, -7, -2, 5, 0 };
        int expectedResult = 2;

        // Act
        int actualResult = input.GetIndexOfMaxBy(x => x*x);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void GetIndexOfMaxBy_TestWithCustomObject_ReturnsCorrectIndex()
    {
        // Arrange
        IEnumerable<Person> input = new List<Person>
        {
            new Person("Alice", 25),
            new Person("Bob", 35),
            new Person("Charlie", 30),
        };
        int expectedResult = 1;

        // Act
        int actualResult = input.GetIndexOfMaxBy(person => person.Age);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}