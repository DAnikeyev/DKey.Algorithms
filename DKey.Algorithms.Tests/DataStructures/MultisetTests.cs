using DKey.Algorithms.DataStructures.Multiset;

namespace DKey.Algorithms.Tests.DataStructures
{
    public class SortedMultisetTests
    {
        [Test]
        public void AddRemoveValues_ExpectValidMultiser()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            ClassicAssert.AreEqual(3, multiset.Count);
            ClassicAssert.AreEqual(2, multiset.CountDistinct);
            ClassicAssert.AreEqual(5, multiset.Max);
            ClassicAssert.AreEqual(3, multiset.Min);

            multiset.Add(3, 3);
            ClassicAssert.AreEqual(6, multiset.Count);

            ClassicAssert.IsTrue(multiset.Remove(3));
            ClassicAssert.AreEqual(5, multiset.Count);
            ClassicAssert.AreEqual(3, multiset.Min);

            ClassicAssert.IsTrue(multiset.Remove(5, 2));
            ClassicAssert.AreEqual(3, multiset.Count);
            ClassicAssert.AreEqual(3, multiset.Max);

            ClassicAssert.IsFalse(multiset.Remove(10));
        }

        [Test]
        public void TryGetValue_ReturnExistedValues()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            ClassicAssert.IsTrue(multiset.TryGetValue(5, out long count));
            ClassicAssert.AreEqual(2, count);

            ClassicAssert.IsTrue(multiset.TryGetValue(3, out count));
            ClassicAssert.AreEqual(1, count);

            ClassicAssert.IsFalse(multiset.TryGetValue(10, out count));
            ClassicAssert.AreEqual(0, count);
        }

        [Test]
        public void GetDistinctItems_ReturnsExpectedCollection()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            var distinctItems = multiset.GetDistinctItems();
            var expected = new List<int> { 3, 5 };
            ClassicAssert.AreEqual(expected, distinctItems);
        }

        [Test]
        public void T04_InitializeWithDictionary_Tests()
        {
            var countDictionary = new Dictionary<int, long>
            {
                { 1, 2 },
                { 2, 3 },
                { 3, 1 }
            };

            var multiset = new SortedMultiset<int>(countDictionary);

            ClassicAssert.AreEqual(6, multiset.Count);
            ClassicAssert.AreEqual(3, multiset.CountDistinct);
            ClassicAssert.AreEqual(3, multiset.Max);
            ClassicAssert.AreEqual(1, multiset.Min);
        }
        [Test]

        public void T05_Remove_Tests()
        {
            var multiset = new SortedMultiset<int>();
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);
            multiset.Add(7);

            ClassicAssert.IsFalse(multiset.Remove(10));
            ClassicAssert.AreEqual(4, multiset.Count);

            ClassicAssert.IsTrue(multiset.Remove(3));
            ClassicAssert.AreEqual(3, multiset.Count);
            ClassicAssert.IsFalse(multiset.TryGetValue(3, out _));

            ClassicAssert.IsTrue(multiset.Remove(5));
            ClassicAssert.AreEqual(2, multiset.Count);
            ClassicAssert.IsTrue(multiset.TryGetValue(5, out var count));
            ClassicAssert.AreEqual(1, count);

            ClassicAssert.IsFalse(multiset.Remove(7, 2));
            ClassicAssert.AreEqual(1, multiset.Count);
            ClassicAssert.IsFalse(multiset.TryGetValue(7, out _));
        }
    }
}
