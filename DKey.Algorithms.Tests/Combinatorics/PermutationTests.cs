using DKey.Algorithms.Combinatorics;

namespace DKey.Algorithms.Tests.Combinatorics;

[TestFixture]
public class PermutationsTests
{
    [Test]
    public void Inverse_ValidPermutation_ReturnsInverse()
    {
        var permutation = new List<int> { 2, 0, 1 };
        var expected = new[] { 1, 2, 0 };
        var actual = Permutations.Inverse(permutation);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Inverse_EmptyPermutation_ReturnsEmptyArray()
    {
        var permutation = new List<int>();
        var expected = new int[0];

        var actual = Permutations.Inverse(permutation);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Multiply_ValidPermutations_ReturnsProduct()
    {
        var permutation1 = new List<int> { 2, 0, 1 };
        var permutation2 = new List<int> { 1, 2, 0 };
        var expected = new[] { 0, 1, 2 };

        var actual = Permutations.Multiply(permutation1, permutation2);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Multiply_EmptyPermutations_ReturnsEmptyArray()
    {
        var permutation1 = new List<int>();
        var permutation2 = new List<int>();
        var expected = Array.Empty<int>();

        var actual = Permutations.Multiply(permutation1, permutation2);

        Assert.AreEqual(expected, actual);
    }
}
