using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
            new BitmapImage(new Uri("src/assets/Jblock2.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Lblock3.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Oblock4.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Sblock5.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Tblock6.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Zblock7.png", UriKind.Relative)),
        };

        private readonly ImageSource[] blockIcons = new ImageSource[]
{
            new BitmapImage(new Uri("src/assets/IconEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Iblock1Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Jblock2Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Lblock3Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Oblock4Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Sblock5Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Tblock6Icon.png", UriKind.Relative)),
            new BitmapImage(new Uri("src/assets/Zblock7Icon.png", UriKind.Relative)),
};

        private int startDelay = 500;
        private int endDelay = 25;
        private int delayFactor = 10;

        private readonly Image[,] imageControls;

        Uri blockPlaceSound = new Uri("pack://siteoforigin:,,,/src/sounds/blockPlace.mp3");
        // SoundPlayer blockPlace = new SoundPlayer(("src/sounds/blockPlace.mp3"));
        MediaPlayer blockPlace = new MediaPlayer();

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

        private void Draw(GameState gameState)
        {
            DrawBoard(gameState.Board);
            DrawGhostBlock(gameState.ActiveBlock);
            DrawBlock(gameState.ActiveBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHoldBlock(gameState.holdBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
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

        private void DrawNextBlock(Queue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockIcons[next.BlockId];
        }

        private void DrawHoldBlock(Block block)
        {
            if (block == null)
            {
                HoldImage.Source = blockIcons[0];
            }
            else
            { 
                HoldImage.Source = blockIcons[block.BlockId];
            }
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
                int delay = Math.Max(endDelay, startDelay - (gameState.Score * delayFactor));
                await (Task.Delay(delay));
                gameState.MoveDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
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
                case Key.C:
                    gameState.HoldBlock();
                    break;
                case Key.Space:
                    gameState.HardDrop();
                    // blockPlace.Load();
                    blockPlace.Open(new Uri(@"/src/sounds/blockPlace.mp3", UriKind.Relative));
                    blockPlace.Play();
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
