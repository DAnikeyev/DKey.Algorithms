using DKey.Algorithms.DataStructures.Interval;

namespace DKey.Algorithms.Tests.DataStructures
{
    [TestFixture]
    public class PairPointsConvererTests
    {
        [Test]
        public void OneInterval_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(1, 4)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 1), smallestCover);
        }

        [Test]
        public void TwoIntervals_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 2), smallestCover);
        }

        [Test]
        public void ThreeIntervals_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5), (3, 6)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void FourIntervals_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(1, 4), (2, 5), (3, 6), (2, 7)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void FiveIntervals_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(1, 7), (2, 6), (3, 3), (3, 4), (2, 9)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((1, 3), smallestCover);
        }

        [Test]
        public void SomeIntervals_ReturnsExpectedCover()
        {
            var intervals = new List<(long left, long right)> {(0, 0), (3, 100)};
            var pairPointsCoverer = new PairPointsCoverer(intervals);
            var smallestCover = pairPointsCoverer.GetSmallestCover();
            Assert.AreEqual((0, 3), smallestCover);
        }
    }
}