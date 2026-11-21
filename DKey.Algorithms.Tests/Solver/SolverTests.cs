using DKey.Algorithms.TextProcessing;

namespace DKey.Algorithms.Tests.Solvers;

[TestFixture]
public class SolverTests
{
    // Each scalar arg consumes one console line (ReadInt takes the first token);
    // a List<int> arg consumes one space-separated line.
    private class SumSolver : Solver
    {
        public SumSolver() : base(new[] { typeof(int), typeof(int) })
        {
        }

        public override void Solve(object[] objects)
        {
            output.AddLine((int)objects[0] + (int)objects[1]);
        }
    }

    private class ListSumSolver : Solver
    {
        public ListSumSolver() : base(new[] { typeof(List<int>) })
        {
        }

        public override void Solve(object[] objects)
        {
            output.AddLine(((List<int>)objects[0]).Sum());
        }
    }

    private class TypedArgsSolver : Solver
    {
        public TypedArgsSolver() : base(new[] { typeof(Skip), typeof(int), typeof(long), typeof(List<int>) })
        {
        }

        public override void Solve(object[] objects)
        {
            output.AddLine(objects.Length);
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
    public void Run_ScalarArgsPerLine_SolvesAndPrints()
    {
        Console.SetIn(new StringReader("3\n4\n"));
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new SumSolver();

        solver.Run();

        ClassicAssert.AreEqual("7\n", stringWriter.ToString());
    }

    [Test]
    public void Run_ListArgFromOneLine_SolvesAndPrints()
    {
        Console.SetIn(new StringReader("3 4 5\n"));
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new ListSumSolver();

        solver.Run();

        ClassicAssert.AreEqual("12\n", stringWriter.ToString());
    }

    [Test]
    public void RunWithArgs_SolvesWithoutPrinting_WhenPrintIsFalse()
    {
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new SumSolver();

        solver.RunWithArgs(new object[] { 5, 7 });

        // Nothing hits the console, but the answer is captured in the builder.
        ClassicAssert.AreEqual(string.Empty, stringWriter.ToString());
        ClassicAssert.AreEqual("12\n", solver.output.ToString());
    }

    [Test]
    public void RunWithArgs_PrintsOutput_WhenPrintIsTrue()
    {
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        var solver = new SumSolver();

        solver.RunWithArgs(new object[] { 5, 7 }, print: true);

        ClassicAssert.AreEqual("12\n", stringWriter.ToString());
    }

    [Test]
    public void Parse_RespectsDeclaredArgTypes()
    {
        Console.SetIn(new StringReader("ignored line\n5\n7\n1 2 3\n"));
        var solver = new TypedArgsSolver();

        var args = solver.Parse();

        ClassicAssert.AreEqual(4, args.Length);
        ClassicAssert.AreEqual("ignored line", args[0]);
        ClassicAssert.IsInstanceOf<int>(args[1]);
        ClassicAssert.AreEqual(5, args[1]);
        ClassicAssert.IsInstanceOf<long>(args[2]);
        ClassicAssert.AreEqual(7L, args[2]);
        ClassicAssert.IsInstanceOf<List<int>>(args[3]);
        ClassicAssert.AreEqual(new[] { 1, 2, 3 }, (List<int>)args[3]);
    }
}
