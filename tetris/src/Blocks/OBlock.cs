﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src.Blocks
{
    public class OBlock : Block
    {
        public override Coordinate[][] Coordinates => coordinates;
        public override int BlockId => 4;

        public override Coordinate Offset
        {
            get { return new(4, 0); }
            set { base.Offset = value; }
        }

        private readonly Coordinate[][] coordinates = new Coordinate[][]
        {
            new Coordinate[] {new(0, 0), new(1, 0), new(0, 1), new(1, 1)},
            new Coordinate[] {new(0, 0), new(1, 0), new(0, 1), new(1, 1)},
            new Coordinate[] {new(0, 0), new(1, 0), new(0, 1), new(1, 1)},
            new Coordinate[] {new(0, 0), new(1, 0), new(0, 1), new(1, 1)}
        };

    }
}

