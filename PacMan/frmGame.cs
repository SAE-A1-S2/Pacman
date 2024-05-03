using Engine;
using Engine.utils;

namespace PacMan
{
    public partial class frmGame : Form
    {

        private GameManager gameManager;

        public frmGame(bool isStoryMode = false)
        {
            InitializeComponent();
            gameManager = new GameManager(isStoryMode);
        }

        private void frmGame_Closed(object sender, EventArgs e)
        {
            Application.OpenForms[0]?.Show();
        }


        public void DisplayMaze(Cell[,] maze)
        {
            int cellSize = 20;
            Bitmap bmp = new(maze.GetLength(0) * cellSize, maze.GetLength(1) * cellSize);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    for (int y = 0; y < maze.GetLength(1); y++)
                    {
                        Color color = maze[x, y] == Cell.Wall ? Color.Black : maze[x, y] == Cell.Ghost ? Color.Blue : maze[x, y] == Cell.John ? Color.Yellow : Color.White;
                        g.FillRectangle(new SolidBrush(color), x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }
            imgMap.Image = bmp;
        }

        private void HandleKeyInput(Keys key)
        {
            var keyToDirectionMap = new Dictionary<Keys, Direction>
            {
                { Keys.Z, Direction.UP },
                { Keys.Up, Direction.UP },
                { Keys.Down, Direction.DOWN },
                { Keys.S, Direction.DOWN },
                { Keys.Left, Direction.LEFT },
                { Keys.Q, Direction.LEFT },
                { Keys.Right, Direction.RIGHT },
                { Keys.D, Direction.RIGHT }
            };

            if (keyToDirectionMap.TryGetValue(key, out Direction direction))
                gameManager.LevelManager.player.Move(direction, gameManager.LevelManager.LevelMap, gameManager);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            HandleKeyInput(keyData);
            DisplayMaze(gameManager.LevelManager.LevelMap);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            DisplayMaze(gameManager.LevelManager.LevelMap);
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            // gameManager.HandleKeyInput(e.KeyCode);
            // DisplayMaze(gameManager.LevelManager.LevelMap);
        }

        private void TmrGhost_Tick(object sender, EventArgs e)
        {
            DisplayMaze(gameManager.LevelManager.LevelMap);
        }
    }
}
