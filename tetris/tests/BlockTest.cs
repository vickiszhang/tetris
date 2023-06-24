using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using tetris.src.Blocks;
using tetris.src;
using NUnit.Framework.Constraints;
using System.Windows.Documents;

namespace tetris.tests
{

    [TestFixture]
    public class BlockTests
    {
        private src.Blocks.Block tblock = null!;

        [SetUp]
        public void Setup()
        {
            tblock = new TBlock();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) }, 
                tblock.DefaultOrientation);
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) },
                tblock.CurrentOrientation);
            Assert.AreEqual(new Coordinate (0, 0), tblock.position);
            Assert.AreEqual(1, src.Blocks.Block.BlockCount);
            Assert.AreEqual(1, tblock.BlockId);
        }

        [Test]
        public void TestMove()
        {
            tblock.Move(5, 5);
            Assert.AreEqual(new Coordinate(5, 5), tblock.position);
            tblock.Move(-7, -4);
            Assert.AreEqual(new Coordinate(-2, 1), tblock.position);
        }

        [Test]
        public void TestRotateRight()
        {
            tblock.RotateRight();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(0, 0), new(1, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateRight();
            Assert.AreEqual(new Coordinate[] { new(-1, 0), new(0, 0), new(1, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateRight();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateRight();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) },
                tblock.CurrentOrientation);
        }

        public void TestRotateLeft()
        {
            tblock.RotateLeft();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateLeft();
            Assert.AreEqual(new Coordinate[] { new(-1, 0), new(0, 0), new(1, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateLeft();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(0, 0), new(1, 0), new(0, 1) },
                tblock.CurrentOrientation);
            tblock.RotateLeft();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) },
                tblock.CurrentOrientation);
        }

        [Test]
        public void TestResetOrientation()
        {
            tblock.RotateRight();
            tblock.RotateRight();
            tblock.ResetOrientation();
            Assert.AreEqual(new Coordinate[] { new(0, -1), new(-1, 0), new(0, 0), new(1, 0) },
                tblock.CurrentOrientation);
        }

    }

}
