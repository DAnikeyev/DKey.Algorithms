using DKey.Algorithms.Search;

namespace DKey.Algorithms.Tests.Search;

[TestFixture]
public class BinarySearchTests
{
    [Test]
    public void CubicRoot_ReturnsExpectedIndex()
    {
        Assert.AreEqual(101, BinarySearch.GetIndex(0, 250, x => x*x*x - 1000000));
    }
    
    [Test]
    public void FlatLine_ReturnsExpectedIndex()
    {
        Assert.AreEqual(3, BinarySearch.GetIndexLong(3, 250, x => 1000000));
    }
    
    [Test]
    public void LastElementIsOptimal_ReturnsExpectedIndex()
    {
        Assert.AreEqual(251, BinarySearch.GetIndexLong(3, 250, x => -x));
    }
}