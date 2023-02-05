``` ini

BenchmarkDotNet=v0.13.4, OS=Windows 11 (10.0.22000.1455/21H2)
AMD Ryzen 9 5900HX with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


```
|    Method |      Mean |    Error |    StdDev |    Median | Rank |   Gen0 | Allocated |
|---------- |----------:|---------:|----------:|----------:|-----:|-------:|----------:|
| ToBinary3 |  27.29 ns | 0.569 ns |  1.530 ns |  26.79 ns |    1 | 0.0181 |     152 B |
| ToBinary1 |  99.46 ns | 4.677 ns | 13.570 ns |  97.37 ns |    2 | 0.0286 |     240 B |
| ToBinary2 | 159.08 ns | 3.106 ns |  4.039 ns | 158.02 ns |    3 | 0.0172 |     144 B |
