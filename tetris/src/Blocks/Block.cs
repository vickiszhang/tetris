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
        public abstract Coordinate[][] Coordinates { get; }
        public abstract int BlockId { get; }

        protected Coordinate offset = new(4, 1);

        public virtual Coordinate Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Coordinate[] DefaultOrientation { get; set; }

        public Coordinate[] CurrentOrientation { get; set; }

        public static int BlockCount { get; set; } = 0;



        protected Block()
        {
            BlockCount++;
            DefaultOrientation = Coordinates[0];
            CurrentOrientation = Coordinates[0];
            
        }

        public IEnumerable<Coordinate> WithOffset()
        {
            foreach (Coordinate c in CurrentOrientation)
            {
                yield return new Coordinate(c.X + offset.X, c.Y + offset.Y);
            }
        }

        public void Move(int x, int y)
        {
            Offset.X += x;
            Offset.Y += y;
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

        public void ResetOrientation()
        {
            CurrentOrientation = DefaultOrientation;
        }
    }
}
