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
    public void ModularPower_ReturnsExpectedValue()
    {
        Assert.AreEqual(5,Arithmetics.Power(3,3));
    }
    [Test] 
    public void ModularInverse_ReturnsExpectedValue()
    {
        Assert.AreEqual(3,Arithmetics.Inverse(4));
    }
    [Test] 
    public void ModularAdd_ReturnsExpectedValue()
    {
        Assert.AreEqual(2,Arithmetics.Add(4,9));
    }
    [Test] 
    public void ModularMultiply_ReturnsExpectedValue()
    {
        Assert.AreEqual(5, Arithmetics.Multiply(4,4));
    }
    [Test] 
    public void ModularDiv_ReturnsExpectedValue()
    {
        Assert.AreEqual(6,Arithmetics.Divide(2,4));
    }

    [Test]
    public void Power_ExpectFastCalculation()
    {
        for (var i = 0; i < 10000000; i++)
        {
            var z = Arithmetics.Power(3, 123124);
        }
    }   
    
    [Test] 
    public void PascalTriangle_CalculatedCorrectly()
    {
        var triangle = ModularArithmetics.MakePascalTriangle(20, 1_000_000_007);
        Assert.AreEqual(1,triangle[0][0]);
        Assert.AreEqual(1,triangle[20][20]);
        var  arithmetics = new ModularArithmetics(1_000_000_007);
        Assert.AreEqual(arithmetics.Choose(20,16), triangle[20][16]);
    }
}