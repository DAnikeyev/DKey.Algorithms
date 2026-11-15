namespace DKey.Algorithms.Tests.Solvers;

[TestFixture]
public class MultiSolverTests
{
    private class SumLinesSolver : MultiSolver
    {
        public bool Initialized;

        public SumLinesSolver() : base(new[] { typeof(List<int>) })
        {
        }

        public override void Init()
        {
            Initialized = true;
        }

        public override void Solve(object[] objects)
        {
            output.AddLine(((List<int>)objects[0]).Sum());
        }
    }

    private TextReader _originalIn = null!;
    private TextWriter _originalOut = null!;

    [SetUp]
    public void SetUp()
    {
        _originalIn = Console.In;
        _originalOut = Console.Out;
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetIn(_originalIn);
        Console.SetOut(_originalOut);
    }

    [Test]
    public void Run_ExecutesInitThenIterationCountTimes_AndPrintsAllAnswers()
    {
        Console.SetIn(new StringReader("2\n1 2\n3 4\n"));
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new SumLinesSolver();

        solver.Run();

        Assert.IsTrue(solver.Initialized);
        Assert.AreEqual("3\n7\n", stringWriter.ToString());
    }

    [Test]
    public void Run_ZeroIterations_PrintsNothing()
    {
        Console.SetIn(new StringReader("0\n"));
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new SumLinesSolver();

        solver.Run();

        Assert.IsTrue(solver.Initialized);
        Assert.AreEqual(string.Empty, stringWriter.ToString());
    }
}
