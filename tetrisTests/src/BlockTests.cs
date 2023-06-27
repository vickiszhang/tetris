using Microsoft.VisualStudio.TestTools.UnitTesting;
using tetris.src;

namespace tetrisTests.src
{
    [TestClass]
    public class BlockTests
    {
        private Block tblock = null!;

        [TestInitialize]
        public void Setup()
        {
            tblock = new TBlock();
        }

        [TestMethod]
        public void TestConstructor()
        {
            Coordinate[] default_expected = new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) };
            Assert.IsTrue(default_expected.SequenceEqual(tblock.DefaultOrientation));

            Assert.IsTrue(default_expected.SequenceEqual(tblock.CurrentOrientation));

            Assert.AreEqual(new Coordinate(4, 1), tblock.Offset);
            Assert.AreEqual(1, Block.BlockCount);
            Assert.AreEqual(6, tblock.BlockId);
        }

        [TestMethod]
        public void TestMove()
        {
            tblock.Move(5, 5);
            Assert.AreEqual(new Coordinate(5 + 4, 5 + 1), tblock.Offset);
            tblock.Move(-7, -4);
            Assert.AreEqual(new Coordinate(9 - 7, 6 - 4), tblock.Offset);
        }

        [TestMethod]
        public void TestRotateRight()
        {

            Coordinate[] right_expected = new Coordinate[] { new(0, -1), new(0, 0), new(1, 0), new(0, 1) };
            Coordinate[] down_expected = new Coordinate[] { new(-1, 0), new(0, 0), new(1, 0), new(0, 1) };
            Coordinate[] left_expected = new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(0, 1) };

            tblock.RotateRight();
            Assert.IsTrue(right_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateRight();
            Assert.IsTrue(down_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateRight();
            Assert.IsTrue(left_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateRight();
            Assert.IsTrue(tblock.DefaultOrientation.SequenceEqual(tblock.CurrentOrientation));

        }

        [TestMethod]
        public void TestRotateLeft()
        {

            Coordinate[] right_expected = new Coordinate[] { new(0, -1), new(0, 0), new(1, 0), new(0, 1) };
            Coordinate[] down_expected = new Coordinate[] { new(-1, 0), new(0, 0), new(1, 0), new(0, 1) };
            Coordinate[] left_expected = new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(0, 1) };

            tblock.RotateLeft();
            Assert.IsTrue(left_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateLeft();
            Assert.IsTrue(down_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateLeft();
            Assert.IsTrue(right_expected.SequenceEqual(tblock.CurrentOrientation));
            tblock.RotateLeft();
            Assert.IsTrue(tblock.DefaultOrientation.SequenceEqual(tblock.CurrentOrientation));

        }

        [TestMethod]
        public void TestResetOrientation()
        {
            tblock.RotateRight();
            tblock.RotateRight();
            tblock.ResetOrientation();
            Assert.IsTrue(tblock.DefaultOrientation.SequenceEqual(tblock.CurrentOrientation));

        }

    }
}