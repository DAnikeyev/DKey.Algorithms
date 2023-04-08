using DKey.Algorithms.DataStructures.Interval;

namespace DKey.Algorithms.Tests
{
    [TestFixture]
    public class PairPointsConvererTests
    {
        [Test]
        public void T01_OneInterval()
        {
            var intervals = new List<(long left, long right)> {(1, 4)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 1), smallestCover);
        }

        [Test]
        public void T02_TwoIntervals()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 2), smallestCover);
        }

        [Test]
        public void T03_ThreeIntervals()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5), (3, 6)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void T04_FourIntervals()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5), (3, 6), (2, 7)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void T05_FiveIntervals()
        {
            var intervals = new List<(long left, long right)> {(1, 7), (2, 6), (3, 3), (3, 4), (2, 9)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void T06_SomeIntervals()
        {
            var intervals = new List<(long left, long right)> {(0, 0), (3, 100)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((0, 3), smallestCover);
        }
    }
}