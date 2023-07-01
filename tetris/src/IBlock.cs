using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src
{
    public class IBlock : Block
    {
        public override Coordinate[][] Coordinates => coordinates;
        public override int BlockId => 1;

        private Coordinate offset = new Coordinate(4, 0);

        public override Coordinate Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private readonly Coordinate[][] coordinates = new Coordinate[][]
        {
            new Coordinate[] {new(-1, 0), new(0, 0), new(1, 0), new(2, 0)},
            new Coordinate[] {new(1, -1), new(1, 0), new(1, 1), new(1, 2)},
            new Coordinate[] {new(-1, 1), new(0, 1), new(1, 1), new(2, 1)},
            new Coordinate[] {new(0, -1), new(0, 0), new(0, 1), new(0, 2)}
        };

        public override void ResetOrientation()
        {
            CurrentOrientation = DefaultOrientation;
            Offset = new Coordinate(4, 0); 
        }

    }

}

