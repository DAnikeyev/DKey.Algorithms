using DKey.Algorithms.NumberTheory;

namespace DKey.Algorithms.Tests.NumberTheory;

[TestFixture]
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
    [Test] 
    public void T03_ModularAdd()
    {
        Assert.AreEqual(2,Arithmetics.Add(4,9));
    }
    [Test] 
    public void T04_ModularMultiply()
    {
        Assert.AreEqual(5, Arithmetics.Multiply(4,4));
    }
    [Test] 
    public void T05_ModularDiv()
    {
        Assert.AreEqual(6,Arithmetics.Divide(2,4));
    }
}