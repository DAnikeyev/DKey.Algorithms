using DKey.Algorithms.DataStructures.Graph.SuffixTree;
using DKey.Algorithms.RandomData;

namespace DKey.Sandbox
{
    internal class Process
    {
        public static void Run(int value)
        {
            var data = ListGenerator.Instance().RandomString(value, 5);
            var tree = SuffixTree<char>.Build(data.ToCharArray(), char.MinValue);
            var ok = tree.Contains("bacd".ToCharArray());
        }
    }
}
