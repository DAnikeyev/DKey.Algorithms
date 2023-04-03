using DKey.Algorithms.IEnumerableExtensions;

namespace DKey.Algorithms.Tests;

public class GetFirstIndexInSortedListInIntervalTests
{
    
    [Test]
    public void T01_GetFirstIndexInSortedListInInterval_TestWithIntegers_ReturnsCorrectIndex()
    {
        // Arrange
        IList<int> input = new List<int> { 1, 3, -7, 9, 15 };
        int min = 16;
        int max = 100;
        int expectedResult = 2;

        // Act
        int actualResult = input.GetFirstIndexInSortedListInInterval(min, max, x => x * x);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void T02_GetFirstIndexInSortedListInInterval_TestWithCustomObject_ReturnsCorrectIndex()
    {
        // Arrange
        IList<Person> input = new List<Person>
        {
            new Person("Alice", 25),
            new Person("Bob", 35),
            new Person("Charlie", 45),
            new Person("David", 55),
            new Person("Eve", 65)
        };
        int min = 30;
        int max = 50;
        int expectedResult = 1;

        // Act
        int actualResult = input.GetFirstIndexInSortedListInInterval(min, max, person => person.Age);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

}