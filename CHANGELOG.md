## 1.6.0 - unreleased
* **Added** `UnionFind` (disjoint-set union with union by rank + path compression) in `DataStructures/DisjointSet`.
* **Added** `TopologicalSort` (Kahn's algorithm) in `DataStructures/Graph/GenericAlgorithms` — returns null on cyclic input.
* **Added** `KmpSearch` in the new `Strings` area: prefix function + first/all (overlapping) substring search, generic over element type, O(n + m).
* **Added** `ZAlgorithm` in `Strings`: Z-array (z[0] = n convention) and overlapping-match search without sentinel concatenation, O(n + m).
* **Added** `LongestIncreasingSubsequence` in the new `DynamicProgramming` area: O(n log n) length + reconstruction, strict/non-strict semantics, comparer-based.

## 1.5.0 - 2026.11.17
* **Added** `TupleHelper` as the canonical (and documented) name for the tuple helpers; the misspelled `TupleHelpler` remains as an `[Obsolete]` forwarding bridge (removal planned for a future major).
* **Fixed** Typos in test namespaces/files: `SufixTree` → `SuffixTree`, `PairPointsConvererTests` → `PairPointsCovererTests` (library API unchanged).
* **Fixed** A stray misspelled reference in the Contest2103 solver template.

## 1.4.0 - 2025.04.22
* **Added** SortedArray methods.
* **Added** LongSum LinqExtension methods.
* **Added** Tuple Helper.
* **Added** Miscellaneous tests.
* **Added** Raw codeforces submissions examples.
* **Modified** Projects are now .NET 8.
* **Modified** Updated codeforces templates for faster Solver editing.

## 1.3.0 - 2024.10.03
* **Added** Red-Black Tree.
* **Added** Dijkstra algorithm.
* **Added** More tests.
* **Updated** TernarySearch.
* **Updated** CodeForcesTemplateBuilder now includes link to the problem.
* **Optimized** Some Combinatorics and PrimeArithmetics.
* **Modified** TernarySearch.	
* **Refactored** Misc files and tests.
* **Refactored** Test names to follow Microsoft's test naming convention.

## 1.2.0 - 2024.08.05
* **Added** IntegerOperationsSegmentTree.
* **Added** Example to work with SegmentTree.
* **Added** More tests.
* **Updated** Binary Tree.
* **Refactored** Misc files and tests.

## 1.1.0 - 2023.04.10
* **Added** Ternary search.
* **Added** Sorted data search.
* **Added** Permutation operations.
* **Added** Sorted Multiset.
* **Added** ContestProcedure project: It creates submission file and launch Codeforces.Program.Main() simultaneously.
* **Added** Changelog.
* **Added** Codeforces medium-hard problem solution examples with explanations.
* **Updated** Binary Search.
* **Updated** Readme.
* **Updated** Nuget package properties.
* **Refactored** Misc files and tests.

## 1.0.0 - 2023.04.01
* First release.