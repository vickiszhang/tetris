using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src.Blocks
{
    public abstract class Block
    {
        public virtual Coordinate[][] Coordinates { get; } = null!;
        public Coordinate[] DefaultOrientation { get; set; }

        public Coordinate[] CurrentOrientation { get; set; }

        public Coordinate position = new(0, 0);
        public int BlockId { get; set; }

        private static int blockCount = 0;



        protected Block()
        {
            DefaultOrientation = Coordinates[0];
            CurrentOrientation = Coordinates[0];
            BlockId = blockCount;
            blockCount++;

        }

        public void Move(int x, int y)
        {
            position.X += x;
            position.Y += y;
        }

        public void RotateLeft()
        {
            int currentIndex = Array.IndexOf(Coordinates, CurrentOrientation);
            int newIndex = (currentIndex - 1 + Coordinates.Length) % Coordinates.Length;
            CurrentOrientation = Coordinates[newIndex];

        }

        public void RotateRight()
        {
            int currentIndex = Array.IndexOf(Coordinates, CurrentOrientation);
            int newIndex = (currentIndex + 1) % Coordinates.Length;
            CurrentOrientation = Coordinates[newIndex];

        }
    }
}
