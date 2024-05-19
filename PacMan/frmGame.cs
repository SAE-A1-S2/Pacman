using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{

		private GameManager gameManager;
		private Direction currentDirection = Direction.STOP;
		private int animationFrame = 0;

		private readonly Image playerUpWalk1 = Properties.Resources.Espion_LookUpWalk_Frame1;
		private readonly Image playerUpWalk2 = Properties.Resources.Espion_LookUpWalk_Frame2;

		private readonly Image playerDownWalk1 = Properties.Resources.Espion_LookDownWalk_Frame1;
		private readonly Image playerDownWalk2 = Properties.Resources.Espion_LookDownWalk_Frame2;

		private readonly Image playerLeftWalk1 = Properties.Resources.Espion_LookLeftWalk_Frame1;
		private readonly Image playerLeftWalk2 = Properties.Resources.Espion_LookLeftWalk_Frame2;

		private readonly Image playerRightWalk1 = Properties.Resources.Espion_LookRightWalk_Frame1;
		private readonly Image playerRightWalk2 = Properties.Resources.Espion_LookRightWalk_Frame2;

		private readonly Image playerStop = Properties.Resources.Espion_LookDownStop;

		public frmGame(GameMode gameMode)
		{
			InitializeComponent();
			gameManager = new GameManager(gameMode);
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
				for (int y = 0; y < maze.GetLength(0); y++)
				{
					for (int x = 0; x < maze.GetLength(1); x++)
					{
						Color color;
						Image? playerImage = null;
						switch (maze[y, x])
						{
							case Cell.Wall:
								color = Color.Black;
								break;
							case Cell.HealthKit:
							case Cell.SpeedBoost:
							case Cell.Torch:
							case Cell.InvisibilityCloack:
								color = Color.Yellow;
								break;
							case Cell.Key:
								color = Color.Blue;
								break;
							case Cell.John:
								color = Color.Orange;
								playerImage = GetPlayerImage();
								break;
							case Cell.Cain:
								color = Color.Red;
								break;
							default:
								color = Color.White;
								break;
						}
						g.FillRectangle(new SolidBrush(color), y * cellSize, x * cellSize, cellSize, cellSize);
						if (playerImage != null)
							g.DrawImage(playerImage, y * cellSize, x * cellSize, cellSize, cellSize);
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
			{
				currentDirection = direction;
				gameManager.StepPlayer(direction);
			}
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
			gameManager.StepGhosts();
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void TmrPlayer_Tick(object sender, EventArgs e)
		{
			gameManager.StepPlayer(currentDirection);
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private Image GetPlayerImage()
		{
			Image[] images = currentDirection switch
			{
				Direction.UP => [playerUpWalk1, playerUpWalk2],
				Direction.DOWN => [playerDownWalk1, playerDownWalk2],
				Direction.LEFT => [playerLeftWalk1, playerLeftWalk2],
				Direction.RIGHT => [playerRightWalk1, playerRightWalk2],
				_ => [playerStop]
			};

			Image image = images[animationFrame % images.Length];
			animationFrame++;

			return image;
		}
	}
}
