using DKey.Algorithms;

namespace DKey.Algorithms.Tests;

public class BinarySearchTests
{
    
    [Test]
    public void T01_CubicRoot()
    {
        Assert.AreEqual(101, BinarySearch.GetIndex(0, 250, x => x*x*x - 1000000));
    }
}