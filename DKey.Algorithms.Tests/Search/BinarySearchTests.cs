using DKey.Algorithms.Search;

namespace DKey.Algorithms.Tests.Search;

public class BinarySearchTests
{
    
    [Test]
    public void T01_CubicRoot()
    {
        Assert.AreEqual(101, BinarySearch.GetIndex(0, 250, x => x*x*x - 1000000));
    }
    
    [Test]
    public void T02_FlatLine()
    {
        Assert.AreEqual(3, BinarySearch.GetIndexLong(3, 250, x => 1000000));
    }
    
    [Test]
    public void T03_LastElement()
    {
        Assert.AreEqual(251, BinarySearch.GetIndexLong(3, 250, x => -x));
    }
}