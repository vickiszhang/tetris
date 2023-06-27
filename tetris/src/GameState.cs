using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris.src;

namespace tetris.src
{
    public class GameState
    {
        private bool GameOver {  get; set; }
        private int Score { get; set; }
        private Queue BlockQueue { get; }
        private Block ActiveBlock { get; set; }
        private Board Board { get; }

        public GameState()
        {
            GameOver = IsGameOver();
            Score = 0;
            BlockQueue = new Queue();
            ActiveBlock = BlockQueue.NewRandomBlock();
            Board = new Board(rows: 22, columns: 10);
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
            Board.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                ActiveBlock = BlockQueue.NewRandomBlock();
            }
        }

    }
}
