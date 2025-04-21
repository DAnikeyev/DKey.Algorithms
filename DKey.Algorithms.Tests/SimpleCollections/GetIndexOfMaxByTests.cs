using DKey.Algorithms.Search;

namespace DKey.Algorithms.Tests.SimpleCollections;

[TestFixture]
public class GetIndexOfMaxByTests
{
    [Test]
    public void GetIndexOfMaxBy_TestWithIntegers_ReturnsCorrectIndex()
    {
        var input = new List<int> { 1, 3, -7, -2, 5, 0 };
        var expectedResult = 2;

        var actualResult = SortedDataSearch.GetIndexOfMaxBy(input, x => x*x);

        Assert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void GetIndexOfMaxBy_TestWithCustomObject_ReturnsCorrectIndex()
    {
        var input = new List<Person>
        {
            new Person("Alice", 25),
            new Person("Bob", 35),
            new Person("Charlie", 30),
        };
        var expectedResult = 1;

        var actualResult = SortedDataSearch.GetIndexOfMaxBy(input, person => person.Age);

        Assert.AreEqual(expectedResult, actualResult);
    }
}