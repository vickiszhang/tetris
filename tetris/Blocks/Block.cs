using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris.Blocks
{
    public abstract class Block
    {
        public Coordinate[][] Coordinates { get; set; }

        protected Block()
        {
            Coordinates = 
        }
        public void RotateLeft()
        {

        }

        public void RotateRight()
        {

        }
    }
}
