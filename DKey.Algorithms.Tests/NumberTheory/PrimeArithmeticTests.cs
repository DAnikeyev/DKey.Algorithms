﻿using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests.NumberTheory;

[TestFixture]
public class PrimeArithmeticsTests
{
    [TestCase(1, false)]
    [TestCase(2, true)]
    [TestCase(3, true)]
    [TestCase(4, false)]
    [TestCase(19, true)]
    [TestCase(20, false)]
    public void IsPrime_ReturnsExpectedValue(int n, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, PrimeArithmetics.IsPrime(n));
    }

    [Test]
    public void GetPrimeFactors_ReturnsExpectedValue()
    {
        var expectedResult = new List<(int prime, int factor)> {(2, 2), (5, 1)};
        CollectionAssert.AreEqual(expectedResult, PrimeArithmetics.GetPrimeFactors(20));
    }

    [Test]
    public void GetPrimeFactorsForInterval_ReturnsExpectedValue()
    {
        var expectedResult = new List<(int prime, int factor)>[]
        {
            new(),
            new(),
            new() {(2, 1)},
            new() {(3, 1)},
            new() {(2, 2)},
            new() {(5, 1)},
            new() {(2, 1), (3, 1)},
        };
        var result = PrimeArithmetics.GetPrimeFactorsForInterval(6);
        for (int i = 0; i <= 6; i++)
        {
            CollectionAssert.AreEqual(expectedResult[i], result[i]);
        }
    }

    [Test]
    public void GetAllDividersTest_ReturnsExpectedValue()
    {
        var expectedResult = new List<int> {1, 2, 4, 5, 10, 20};
        CollectionAssert.AreEqual(expectedResult, PrimeArithmetics.GetAllDividers(20));
    }

    [Test]
    [Explicit]
    public void GetPrimeFactorsForIntervalBig_ReturnsExpectedValue()
    {
        var result = PrimeArithmetics.GetPrimeFactorsForInterval(1000002);
    }
}