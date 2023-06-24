using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris.src
{
    public class GameState
    {
        private bool GameOver {  get; set; }
        private int Score { get; set; }
        private Queue BlockQueue { get; }
        private Board Board { get; }

        public GameState()
        {
            GameOver = false;
            Score = 0;
            BlockQueue = new Queue();
            Board = new Board(rows: 22, columns: 10);
        }
    }
}
