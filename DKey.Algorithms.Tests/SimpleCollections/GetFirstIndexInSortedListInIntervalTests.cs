using DKey.Algorithms.Search;

namespace DKey.Algorithms.Tests.SimpleCollections;

[TestFixture]
public class GetFirstIndexInSortedListInIntervalTests
{
    [Test]
    public void GetFirstIndexInSortedListInInterval_TestWithIntegers_ReturnsCorrectIndex()
    {
        var input = new List<int> { 1, 3, -7, 9, 15 };
        var min = 16;
        var max = 100;
        var expectedResult = 2;

        var actualResult = SortedDataSearch.GetFirstIndexInSortedListInInterval(input, min, max, x => x * x);

        Assert.AreEqual(expectedResult, actualResult);
    }

    public void GetFirstIndexInSortedListInInterval_TestWithCustomObject_ReturnsCorrectIndex()
    {
        IList<Person> input = new List<Person>
        {
            new Person("Alice", 25),
            new Person("Bob", 35),
            new Person("Charlie", 45),
            new Person("David", 55),
            new Person("Eve", 65)
        };
        var min = 30;
        var max = 50;
        var expectedResult = 1;

        var actualResult = SortedDataSearch.GetFirstIndexInSortedListInInterval(input, min, max, person => person.Age);

        Assert.AreEqual(expectedResult, actualResult);
    }

}