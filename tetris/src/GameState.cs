using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;
using Queue = tetris.src.Queue;

namespace tetris.src
{
    public class GameState
    {
        public bool GameOver {  get; set; }
        public int Score { get; set; }
        public Queue BlockQueue { get; }
        private Block activeBlock;
        public Block holdBlock;
        public bool CanHold { get; private set; }

        public Block ActiveBlock
        {
            get => activeBlock;
            private set
            {
                activeBlock = value;
                activeBlock.ResetOrientation();
            }
        }

        public Board Board { get; }

        public GameState()
        {
            BlockQueue = new Queue();
            ActiveBlock = BlockQueue.NewRandomBlock();
            Board = new Board(rows: 23, columns: 10);
            CanHold = true;
        }

        private bool IsGameOver()
        {
            return !Board.IsRowEmpty(0) && !Board.IsRowEmpty(1);
        }

        private bool WithinBoundary()
        {
            foreach (Coordinate c in ActiveBlock.WithOffset())
            {
                if (!Board.IsEmpty(c.Y, c.X)) // TODO: fix this change rc to xy
                {
                    return false;
                }
            }
            return true;
        }

        public void RotateRight()
        {
            ActiveBlock.RotateRight();
            if (!WithinBoundary())
            {
                ActiveBlock.RotateLeft();
            }
        }
        public void RotateLeft()
        {
            ActiveBlock.RotateLeft();
            if (!WithinBoundary())
            {
                ActiveBlock.RotateRight();
            }
        }

        public void MoveRight()
        {
            ActiveBlock.Move(1, 0);
            if (!WithinBoundary())
            {
                ActiveBlock.Move(-1, 0);
            }
        }

        public void MoveLeft()
        {
            ActiveBlock.Move(-1, 0);
            if (!WithinBoundary())
            {
                ActiveBlock.Move(1, 0);
            }
        }

        public void MoveDown()
        {
            ActiveBlock.Move(0, 1);
            if (!WithinBoundary())
            {
                ActiveBlock.Move(0, -1);
                PlaceBlock();
            }
        }

        public void PlaceBlock()
        {
            foreach (Coordinate c in ActiveBlock.WithOffset())
            {
                Board[c.Y, c.X] = ActiveBlock.BlockId;
            }
            Score += Board.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                ActiveBlock = BlockQueue.NewRandomBlock();
                CanHold = true;
            }
        }

        public int GetDropDistance()
        {
            int dropDistance = Board.Rows;
            foreach (Coordinate c in ActiveBlock.WithOffset())
            {
                dropDistance = System.Math.Min(dropDistance, GetHardDropDistance(c));
            }

            return dropDistance;


            int GetHardDropDistance(Coordinate c)
            {
                int drop = 0;
                while (Board.IsEmpty(c.Y + drop + 1, c.X))
                {
                    drop++;
                }

                return drop;
            }
        }

        public void HardDrop()
        {
            ActiveBlock.Move(0, GetDropDistance());
            PlaceBlock();
        }

        public void HoldBlock()
        {
            Block tempHoldBlock = holdBlock;
            if (!CanHold)
            {
                return;
            }
            if (holdBlock != null)
            {
                holdBlock = ActiveBlock;
                ActiveBlock = tempHoldBlock;

            }
            else
            {
                holdBlock = ActiveBlock;
                ActiveBlock = BlockQueue.NewRandomBlock();
            }

            CanHold = false;
     
        }
    }
}
