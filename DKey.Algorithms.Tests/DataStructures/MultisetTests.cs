using DKey.Algorithms.DataStructures.Multiset;

namespace DKey.Algorithms.Tests.DataStructures
{
    public class SortedMultisetTests
    {
        [Test]
        public void T01_Add_Remove_Tests()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            Assert.AreEqual(3, multiset.Count);
            Assert.AreEqual(2, multiset.CountDistinct);
            Assert.AreEqual(5, multiset.Max);
            Assert.AreEqual(3, multiset.Min);
            
            multiset.Add(3, 3);
            Assert.AreEqual(6, multiset.Count);

            Assert.IsTrue(multiset.Remove(3));
            Assert.AreEqual(5, multiset.Count);
            Assert.AreEqual(3, multiset.Min);
            
            Assert.IsTrue(multiset.Remove(5, 2));
            Assert.AreEqual(3, multiset.Count);
            Assert.AreEqual(3, multiset.Max);
            
            Assert.IsFalse(multiset.Remove(10));
        }

        [Test]
        public void T02_TryGetValue_Tests()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            Assert.IsTrue(multiset.TryGetValue(5, out long count));
            Assert.AreEqual(2, count);

            Assert.IsTrue(multiset.TryGetValue(3, out count));
            Assert.AreEqual(1, count);

            Assert.IsFalse(multiset.TryGetValue(10, out count));
            Assert.AreEqual(0, count);
        }

        [Test]
        public void T03_GetDistinctItems_Tests()
        {
            var multiset = new SortedMultiset<int>(true);
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);

            var distinctItems = multiset.GetDistinctItems();
            var expected = new List<int> { 3, 5 };
            CollectionAssert.AreEqual(expected, distinctItems);
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

            Assert.AreEqual(6, multiset.Count);
            Assert.AreEqual(3, multiset.CountDistinct);
            Assert.AreEqual(3, multiset.Max);
            Assert.AreEqual(1, multiset.Min);
        }
        [Test]
        
        public void T05_Remove_Tests()
        {
            var multiset = new SortedMultiset<int>();
            multiset.Add(5);
            multiset.Add(3);
            multiset.Add(5);
            multiset.Add(7);

            Assert.IsFalse(multiset.Remove(10));
            Assert.AreEqual(4, multiset.Count);

            Assert.IsTrue(multiset.Remove(3));
            Assert.AreEqual(3, multiset.Count);
            Assert.IsFalse(multiset.TryGetValue(3, out _));

            Assert.IsTrue(multiset.Remove(5));
            Assert.AreEqual(2, multiset.Count);
            Assert.IsTrue(multiset.TryGetValue(5, out var count));
            Assert.AreEqual(1, count);

            Assert.IsFalse(multiset.Remove(7, 2));
            Assert.AreEqual(1, multiset.Count);
            Assert.IsFalse(multiset.TryGetValue(7, out _));
        }
    }
}