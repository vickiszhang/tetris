﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src
{
    public class ZBlock : Block
    {
        public override Coordinate[][] Coordinates => coordinates;
        public override int BlockId => 7;

        private readonly Coordinate[][] coordinates = new Coordinate[][]
        {
            new Coordinate[] {new(-1, -1), new(0, -1), new(0, 0), new(1, 0)},
            new Coordinate[] {new(1, -1), new(0, 0), new(1, 0), new(0, 1)},
            new Coordinate[] {new(-1, 0), new(0, 0), new(0, 1), new(1, 1)},
            new Coordinate[] {new(0, -1), new(-1, 0), new(0, 0), new(-1, 1)}
        };

    }

}

