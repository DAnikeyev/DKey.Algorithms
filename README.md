# DKey.Algorithms
DKey.Algorithms is a library that provides basic algorithms and data structures implementation in C#.

### Currently Implemented:
- **Data Structures**
  - [x] Graph
  - [x] Tree
  - [x] Sorted Multiset
  - [x] Segment Tree
  - [x] Generic Suffix Tree
  - [x] Short Suffix Tree
  - [x] Prefix Trie
  - [x] Red-Black Tree
- **Graph Processing**
  - [x] Depth First Search
  - [x] Breadth First Search
  - [x] Generic graph statistics (Components, Cycle, Shortest path, Diameter)
  - [x] Dijkstra Algorithm
- **Number Theory**
  - [x] Prime Arithmetic
  - [x] Binary Arithmetic
  - [x] Modular Arithmetic
  - [x] Combinatorics
- **Argument Search**
  - [x] Binary Search
  - [x] Ternary Search
  - [x] Sorted Data Search
- **Data Manipulation**
  - [x] IOHelper
  - [x] Tokenizer
  - [x] Random Data Generator (for lists, strings and graphs)
- **Others**
  - [x] Problem Solver and MultiSolver
  - [x] LINQ Extension

### DKey.Codeforces
DKey.Codeforces contains favorite examples of solutions for [Codeforces Problems](http://codeforces.com/) that can be used as a template for solving and testing problems, not limited to Codeforces.


**To run a new solution:**
 - Create a new Solver class
 - The Solver constructor should contain the **types** of parameters that are going to be parsed and delivered in arguments of the **Solve()** method
 - The **Solve()** method should contain the solution logic and use **output.Append()** or **output.AddL()** to append the result to StringBuilder that will print output at the end of the program
 - Swap the Solver class in program.cs to run your solver
 - Use the **MultiSolver** instead, if the problem input starts with number of test cases followed by multiple test cases.

ContestTemplateBuilder can be used to create contest templates to work in this project.

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
DKey.ContestProcedure creates submission file and launch Codeforces.Program.Main() simultaneously. Perfect for checking the test output or debugging right before submitting the solution.

### Thank you for using DKey.Algorithms!
### Contacts: 
**dmitry.anikeyev@gmail.com**