using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using tetris.src.Blocks;
using tetris.src;

namespace tetris.tests
{

    [TestFixture]
    public class BlockTests
    {
        [Test]
        public void DefaultCoordinates_Should_Return_Correct_Values()
        {
            // Arrange
            Block block = new SBlock();

            // Act
            Coordinate[,] coordinates = block.Coordinates;

            // Assert
            Assert.AreEqual(new Coordinate(0, -1), coordinates[0, 0]);
            Assert.AreEqual(new Coordinate(1, -1), coordinates[0, 1]);
            Assert.AreEqual(new Coordinate(-1, 0), coordinates[0, 2]);
            Assert.AreEqual(new Coordinate(0, 0), coordinates[0, 3]);
        }

        [Test]
        public void RotateRight_Should_Update_CurrentPosition_Correctly()
        {
            // Arrange
            Block block = new SBlock();

            // Act
            block.RotateRight();

            // Assert
            Assert.AreEqual(new Coordinate(0, -1), block.CurrentPosition[0]);
            Assert.AreEqual(new Coordinate(0, 0), block.CurrentPosition[1]);
            Assert.AreEqual(new Coordinate(1, 0), block.CurrentPosition[2]);
            Assert.AreEqual(new Coordinate(1, 1), block.CurrentPosition[3]);
        }

        [Test]
        public void RotateLeft_Should_Update_CurrentPosition_Correctly()
        {
            // Arrange
            Block block = new SBlock();

            // Act
            block.RotateLeft();

            // Assert
            Assert.AreEqual(new Coordinate(-1, 0), block.CurrentPosition[0]);
            Assert.AreEqual(new Coordinate(0, 0), block.CurrentPosition[1]);
            Assert.AreEqual(new Coordinate(1, 0), block.CurrentPosition[2]);
            Assert.AreEqual(new Coordinate(-1, 1), block.CurrentPosition[3]);
        }
    }

}
