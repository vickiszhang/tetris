using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using tetris.src;
using Block = tetris.src.Block;

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] blockTiles = new ImageSource[]
        {
            new BitmapImage(new Uri("src/assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Iblock1.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Iblock1.png", UriKind.Relative)), // TODO: get a new colour
            new BitmapImage(new Uri("src/assets/Lblock3.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Oblock4.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Sblock5.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Tblock6.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Zblock7.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;

        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupCanvas(gameState.Board);
        }
        
        private Image[,] SetupCanvas(Board board)
        {
            Image[,] imageControls = new Image[board.Rows, board.Columns];
            int cellSize = 25;
            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imageControl, (r-1) * cellSize);
                    Canvas.SetLeft(imageControl,  c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Columns; c++)
                {
                    int id = board[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = blockTiles[id];
                }
            }

        }

        private void DrawBlock(Block block)
        {
            foreach (Coordinate c in block.WithOffset())
            {
                imageControls[c.Y, c.X].Opacity = 1;
                imageControls[c.Y, c.X].Source = blockTiles[block.BlockId];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawBoard(gameState.Board);
            DrawGhostBlock(gameState.ActiveBlock);
            DrawBlock(gameState.ActiveBlock);
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.GetDropDistance();
            foreach (Coordinate c in block.WithOffset())
            {
                imageControls[c.Y + dropDistance, c.X].Opacity = 0.20;
                imageControls[c.Y + dropDistance, c.X].Source = blockTiles[block.BlockId];
            }
        }

        private async Task GameLoop()
        {
            Draw(gameState);
            while (!gameState.GameOver)
            {
                await (Task.Delay(500));
                gameState.MoveDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveLeft();
                    break;
                case Key.Right:
                    gameState.MoveRight();
                    break;
                case Key.Down:
                    gameState.MoveDown();
                    break;
                case Key.Up:
                    gameState.RotateRight();
                    break;
                case Key.Z:
                    gameState.RotateLeft();
                    break;
                case Key.Space:
                    gameState.HardDrop();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}
