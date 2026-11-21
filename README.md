# DKey.Algorithms
DKey.Algorithms is a library of core algorithms and data structures in C#: graph processing, ordered structures, segment trees, number theory, string structures, search, and contest IO tooling.

Targets .NET 8, zero package dependencies, published on [NuGet](https://www.nuget.org/packages/DKey.Algorithms) as `DKey.Algorithms`. Every public member carries XML documentation, so the API reference shows up in your editor's IntelliSense.

```bash
dotnet add package DKey.Algorithms
```

## Usage by domain

### Graph

Build an adjacency list from edges, then traverse it or run graph algorithms:

```csharp
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.BreadthFirstSearch;
using DKey.Algorithms.DataStructures.Graph.DepthFirstSearch;
using DKey.Algorithms.DataStructures.Graph.GenericAlgorithms;
using DKey.Algorithms.DataStructures.Graph.Misc;

var edges = new List<(int, int)> { (0, 1), (1, 2), (2, 3), (3, 4) };
var graph = GraphBuilder.Undirected(edges, 5);        // List<int>[5]
var (digraph, reversed) = GraphBuilder.Directed(edges, 5); // both directions of a digraph

// BFS with per-vertex depth (TraverseContext tracks parent/depth)
BFS.Traverse(new TraverseContext(graph, 0), c => Console.WriteLine($"{c.CurrentVertex} d={c.Depth}"));

// iterative DFS (DFSContext also exposes Parents/TraverseDepth)
DFS.Iterative(new DFSContext(digraph, 0), c => Console.WriteLine(c.CurrentVertex));

// graph statistics
var components = ConnectedComponents.Get(graph);      // List<List<int>>
var path = ShortestPath.Get(graph, 0, 4);             // List<int>? — vertex path, null when unreachable
var hasCycle = Cycle.Exists(graph);                   // bool — undirected graphs
var cycle = Cycle.Find(graph);                        // List<int>? — vertices of one cycle
var (v1, v2, diameter) = TreeDiameter.Get(graph);     // on trees: farthest pair + hop count

// Dijkstra over a weighted digraph (weights must cover every edge); unreachable keeps long.MaxValue
var wgraph = GraphBuilder.Directed(new List<(int, int)> { (0, 1), (1, 2), (0, 2) }, 3).G;
var weights = new Dictionary<(int, int), int> { [(0, 1)] = 10, [(1, 2)] = 5, [(0, 2)] = 30 };
var dist = Dijkstra.ToAll(new WeightedGraphContext(wgraph, 0, weights));
Console.WriteLine(dist[2]);                           // 15  (0 -> 1 -> 2)
```

### Ordered structures (Red-Black Tree)

An ordered set with rank queries — `Search` returns the rank (0-based order statistic) of a key:

```csharp
using DKey.Algorithms.DataStructures.Ordered;

var tree = new RedBlackTree<string>(StringComparer.Ordinal);
foreach (var word in new[] { "pear", "apple", "fig" }) tree.TryAdd(word);
tree.TryDelete("fig");

var (rank, _) = tree.Search("pear", out var found);   // rank = 1 of 2 keys, found = true
Console.WriteLine($"{tree.Count} keys, min {tree.Min}, max {tree.Max}");

var numbers = new RBTInt();                           // presets: RBTInt / RBTLong / RBTString
numbers.TryAdd(42);
numbers.TryAdd(21);
Console.WriteLine(numbers.Search(42, out _).index);   // 1
```

### Multiset and sorted array

```csharp
using DKey.Algorithms.DataStructures;
using DKey.Algorithms.DataStructures.Multiset;

var ms = new SortedMultiset<int>();                   // sorted multiset with counts
ms.Add(3, 5);                                         // five 3s
ms.Add(7, 2);                                         // two 7s
ms.Remove(3, 2);                                      // drop two of them
Console.WriteLine($"{ms.Count} items, {ms.CountDistinct} distinct, min {ms.Min}");

var sorted = new SortedArray<int>(new[] { 1, 3, 3, 5, 8 });  // wrap pre-sorted data
Console.WriteLine(sorted.CountGreaterThan(3));        // 2  (strictly greater)
```

### Segment trees

Interval addition + sum queries, or plug in a custom aggregation:

```csharp
using DKey.Algorithms.DataStructures.SegmentTree;

var data = new List<int> { 3, 1, 2, 5, 4, 2, 1, 6 };
var sumTree = new IntegerSegmentTree();
sumTree.InitFromIntList(data);
sumTree.AddToInterval(0, 3, 10);                      // += 10 on [0..3]
Console.WriteLine(sumTree.GetCumulativeOperation(2, 4).Value); // 31  (12+15+4)

// custom operation: interval minimum via an index->value dictionary (needs a neutral entry)
var map = new Dictionary<long, long>();
for (var i = 0; i < data.Count; i++) map[i] = data[i];
map[map.Count] = long.MaxValue;
var minTree = new IntegerOperationsSegmentTree((d, a, b) => d[a] < d[b] ? a : b, map);
minTree.InitFromIntList(Enumerable.Range(0, data.Count).ToList());
Console.WriteLine(data[(int)minTree.GetCumulativeOperation(2, 5).Value]); // 2
```

### Number theory

```csharp
using DKey.Algorithms.NumberTheory;

Console.WriteLine(PrimeArithmetics.IsPrime(97));              // true
Console.WriteLine(PrimeArithmetics.GetPrimeFactors(360));     // [(2,3),(3,2),(5,1)]
Console.WriteLine(string.Join(",", PrimeArithmetics.GetAllDividers(12)));

Console.WriteLine(IntArithmetics.GCD(48, 18));                // 6

var mod = new ModularArithmetics();                           // modulus 1_000_000_007 by default
Console.WriteLine(mod.Power(2, 100));                         // 2^100 mod 1e9+7
Console.WriteLine(mod.Choose(10, 3));                         // 120
Console.WriteLine(mod.Divide(6, 2));                          // modular division via inverse

Console.WriteLine(BinaryArithmetics.CountPositiveBits(255));  // 8
Console.WriteLine(BinaryArithmetics.IntToString(10, new[] { '0', '1' })); // 1010
```

### Strings: suffix trees, prefix trie, tokenizer

```csharp
using DKey.Algorithms.DataStructures.Graph;
using DKey.Algorithms.DataStructures.Graph.PrefixTrie;
using DKey.Algorithms.DataStructures.Graph.ShortSuffixTree;
using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.TextProcessing;

// generic suffix tree over any IComparable alphabet
var suffix = SuffixTree<char>.Build("banana".ToList(), char.MinValue);
Console.WriteLine(suffix.Contains("ana".ToList()));           // true

// int-alphabet variant with a smaller footprint
var st = ShortSuffixTree.Build("banana".Select(c => c - 'a').ToList(), 0);
Console.WriteLine(st.Contains(new List<int> { 0, 13 }));      // true ("an" = a,n − 'a')

var trie = new Trie();
trie.Build(new List<string> { "car", "card", "care" });
Console.WriteLine(trie.SearchPrefix("ca"));                   // true — some word starts with it
Console.WriteLine(trie.SearchLongestPrefix("careless"));      // care

Console.WriteLine(string.Join("|", Tokenizer.Split("hello 42 world", TokenizerMode.TakeOnlyLettersOrDigit)));
// hello|42|world
Console.WriteLine(string.Join("|", Tokenizer.Split("abc123 def", TokenizerMode.TakeOnlyLetters)));
// def — letter runs adjacent to digits yield no token (word-boundary semantics)
```

### Search

Binary/ternary search over a monotone/unimodal predicate on integers (not just arrays):

```csharp
using DKey.Algorithms.ArgumentSearch;
using DKey.Algorithms.Search;

// smallest x in [0, 100] with x^2 > 99  (predicate must be monotonically increasing)
Console.WriteLine(BinarySearch.GetIndex(0, 100, x => x * x > 99 ? 1 : -1)); // 10

// ternary search: minimum of a unimodal function on integers
Console.WriteLine(TernarySearch.GetIndex(0, 100, x => (x - 37) * (x - 37))); // 37
// golden-section variant for doubles
Console.WriteLine(TernarySearch.GoldenSection(-10.0, 10.0, x => x * x));     // 0

// helpers over sorted data
var idx = SortedDataSearch.GetFirstIndexInSortedListInInterval(
    new List<int> { 10, 20, 30, 40 }, 15, 35, x => x);        // 1  (first value in (15, 35))
Console.WriteLine(SortedDataSearch.GetIndexOfMaxBy(new[] { 3, 9, 7 }, x => x)); // 1
```

### Combinatorics

```csharp
using DKey.Algorithms.Combinatorics;

var p = new[] { 2, 0, 1 };                       // permutation of {0,1,2}
Console.WriteLine(string.Join(",", Permutations.Inverse(p)));    // 1,2,0
Console.WriteLine(string.Join(",", Permutations.Multiply(p, p))); // 0,1,2
```

### Random data

Deterministic generators (fixed seed ⇒ reproducible test data):

```csharp
using DKey.Algorithms.RandomData;

var gen = ListGenerator.Instance(seed: 42);
Console.WriteLine(string.Join(",", gen.RandomList(5, 1, 100)));  // 5 ints in [1, 100]
Console.WriteLine(string.Join(",", gen.RandomPermutation(5)));   // shuffle of 0..4
Console.WriteLine(gen.RandomString(8, 26));                      // 8 letters

var ggen = GraphGenerator.Instance(seed: 7);
var tree = ggen.RandomTree(100);                 // random labelled tree
var gnp = ggen.RandomGilbert(100, 0.05);         // G(n, p) Erdős–Rényi
var gnm = ggen.RandomErdesRenyi(100, 200);       // n vertices, m edges
```

### Contest IO: Solver and text helpers

`Solver` reads typed arguments from stdin, `MultiSolver` handles the "test count first" format. Output accumulates in the public `output` StringBuilder:

```csharp
using System.Text;
using DKey.Algorithms;

public class SumSolver : Solver
{
    public SumSolver() : base(new[] { typeof(int), typeof(int) }) { }

    public override void Solve(object[] objects)
    {
        var (a, b) = ((int)objects[0], (int)objects[1]);
        output.Append(a + b).AppendLine();
    }
}
// new SumSolver().Run();          // stdin "3 4"  ->  stdout "7"

public class SumMultiSolver : MultiSolver   // stdin "2\n3 4\n10 -1" -> "7\n9"
{
    public SumMultiSolver() : base(new[] { typeof(int), typeof(int) }) { }
    public override void Solve(object[] objects) =>
        output.Append((int)objects[0] + (int)objects[1]).AppendLine();
}
```

The richer contest IO (typed line readers like `ReadIntLine`/`Read2dList` and answer-building extensions) lives in the internal `IOHelper`, available to the in-repo contest projects (`DKey.Codeforces` & friends) — not part of the NuGet surface.

### Small utilities

```csharp
using DKey.Algorithms;
using DKey.Algorithms.LinqExtension;

Console.WriteLine(TupleHelper.Swap((1, 2)));                    // (2, 1)
Console.WriteLine(new[] { int.MaxValue, 1 }.LongSum());         // no int overflow
Console.WriteLine(string.Join(",", new[] { 1, 2, 1 }.ToCountDictionary(x => x))); // [1, 2] -> [2, 1]
```

### DKey.Codeforces
DKey.Codeforces contains favorite examples of solutions for [Codeforces Problems](http://codeforces.com/) that can be used as a template for solving and testing problems, not limited to Codeforces.


**To run a new solution:**
 - Create a new Solver class
 - The Solver constructor should contain the **types** of parameters that are going to be parsed and delivered in arguments of the **Solve()** method
 - The **Solve()** method should contain the solution logic and use **output.Append()** or **output.AddL()** to append the result to StringBuilder that will print output at the end of the program
 - Swap the Solver class in program.cs to run your solver
 - Use the **MultiSolver** instead, if the problem input starts with number of test cases followed by multiple test cases.

ContestTemplateBuilder can be used to create contest templates to work in this project.

## Release process

Releases are CHANGELOG-driven and automated from `main`:

1. Add a `## X.Y.Z - YYYY.MM.DD` section to [CHANGELOG.md](CHANGELOG.md) describing the changes.
2. Bump `<Version>X.Y.Z</Version>` in `DKey.Algorithms/DKey.Algorithms.csproj` to the same version.
3. Merge to `main`. CI publishes the package to NuGet (when the version is new), then creates the `vX.Y.Z` git tag and a GitHub Release whose notes are the CHANGELOG section for that version. Both steps are idempotent — re-runs skip what already exists, and a missing release is created by the next green `main` run.

A version already on `main` but missing its release can also be released manually: `gh release create vX.Y.Z --target <sha> --notes "<CHANGELOG section>"`.

## Utils
### DKey.ContestSubmissionBuilder
DKey.ContestSubmissionBuilder is a console application to build a single file submission for Codeforces problems.  
It takes the EntryPoint of your solution (supposedly **program.cs** located in DKey.Codeforces) as root and builds a single file submission combining the subtree of dependencies.  
Traverse through tree of dependencies is not perfect.  
To use application correctly, please, follow **these rules**
- 1 class = 1 file
- Class name should be the same as the file name
- Class name should contains only letters and digits
- Don't use short class names that can be confused with other keywords and methods (like **List** or **Copy**)
- Use file-scoped namespace declarations like **namespace x;** instead of **namespace x{}**
- Your files should contain class, struct, enum or interface
- Don't name you variables like other classes (usually only problem for public members, as they start with UpperCase)  
- Be careful with static extension methods (with **this** keyword), as their class name is not garanteed to be in dependent class and might be skipped while combining submission. Extension should be in their own namespace and should be added manually to config of ContestSubmissionBuilder.
- Try to avoid complex static classes with many dependencies, if only 1 method might be required from it, as it will lead to a lot of unnecessary dependencies in submission.cs

Please note that violating most of the rules will result in unnecessary classes in submission.cs, but some might break the logic.

### DKey.ContestTemplateBuilder
DKey.ContestTemplateBuilder generates folder Contest{number} in DKey.CodeForces and fill it with 6 solution templates based on MultiSolver for Problems A-F.

### DKey.ContestProcedure
DKey.ContestProcedure creates submission file and launch Codeforces.Program.Main() simultaneously. Perfect for checking the test output or debugging right before submitting your problem.

### Thank you for using DKey.Algorithms!
### Contacts: 
**dmitry.anikeyev@gmail.com**
