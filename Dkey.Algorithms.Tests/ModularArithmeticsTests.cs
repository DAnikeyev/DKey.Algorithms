using DKey.Algorithms.NumberTheory;

namespace Dkey.Algorithms.Tests;

public class ModularArithmeticsTests
{
    public ModularArithmetics Arithmetics;

    [OneTimeSetUp]
    public void Setup()
    {
        Arithmetics = new ModularArithmetics(11);
    }

    [Test] 
    public void T01_ModularPower()
    {
        Assert.AreEqual(5,Arithmetics.Power(3,3));
    }
    [Test] 
    public void T02_ModularInverse()
    {
        Assert.AreEqual(3,Arithmetics.Inverse(4));
    }
}