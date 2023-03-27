
using System.Diagnostics;
using Process = DKey.Sandbox.Process;

Console.WriteLine("Hello, World!");
var sw = Stopwatch.StartNew();
Process.Run(3000000);

;
Console.WriteLine($"elapsed = {sw.ElapsedMilliseconds}");