using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris.Blocks
{
    class SBlock : Block
    {

        public Coordinate[,] coordinates = new Coordinate[,]
        {
            {new(-1, -1), new(0, -1), new(0, 0), new(1, 0)},
            {new(0, -1), new(0, 0), new(1, 0), new(0, 1)},
            {new(-1, 0), new(0, 0), new(1, 0), new(0, 1)},
            {new(0, -1), new(-1, 0), new(0, 0), new(0, 1)}
        };

    }

    XXO
    OXX
    OOO
}

