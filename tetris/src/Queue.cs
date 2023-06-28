using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src
{
    public class Queue
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock(),
        };

        public Block NextBlock { get; private set; }
        private readonly Random random = new Random();

        public Queue()
        {
            NextBlock = GetRandomBlock();
        }

        private Block GetRandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block NewRandomBlock()
        {

            Block block = NextBlock;
            NextBlock = GetRandomBlock();
            return block;
        }
    }
}
