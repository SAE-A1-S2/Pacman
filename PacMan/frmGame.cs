using System.Diagnostics;
using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{

		private GameManager gameManager;
		private Direction currentDirection = Direction.STOP;
		private CellCoordinates PlayerStartPos;
		private CellCoordinates PlayerEndPos;
		private CellCoordinates CainPrevPos, WinstonPrevPos, ViggoPrevPos, MarquisPrevPos;
		private float interpolationFactor;
		private const float interpolationStep = 0.1f;
		private int animationFrame = 0;

		private readonly Dictionary<string, Image[]> characterImages = new()
		{
			{"PlayerUP", new Image[] { Properties.Resources.Espion_LookUpWalk_Frame1, Properties.Resources.Espion_LookUpWalk_Frame2 }},
			{"PlayerDOWN", new Image[] { Properties.Resources.Espion_LookDownWalk_Frame1, Properties.Resources.Espion_LookDownWalk_Frame2 }},
			{"PlayerLEFT", new Image[] { Properties.Resources.Espion_LookLeftWalk_Frame1, Properties.Resources.Espion_LookLeftWalk_Frame2 }},
			{"PlayerRIGHT", new Image[] { Properties.Resources.Espion_LookRightWalk_Frame1, Properties.Resources.Espion_LookRightWalk_Frame2 }},
			{"PlayerStop", new Image[] { Properties.Resources.Espion_LookDownStop }},
			{"CainSTOP", new Image[] { Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2 }},
			{"WinstonSTOP", new Image[] { Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2 }},
			{"ViggoSTOP", new Image[] { Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2 }},
			{"MarquisSTOP", new Image[] { Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2 }},
			{"CainUP", new Image[] { Properties.Resources.CainFaceUp1, Properties.Resources.CainFaceUp2 }},
			{"CainDOWN", new Image[] { Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2 }},
			{"CainLEFT", new Image[] { Properties.Resources.CainLeft1, Properties.Resources.CainLeft2 }},
			{"CainRIGHT", new Image[] { Properties.Resources.CainRight1, Properties.Resources.CainRight2 }},
			{"WinstonUP", new Image[] { Properties.Resources.WinstonUp1, Properties.Resources.WinstonUp2 }},
			{"WinstonDOWN", new Image[] { Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2 }},
			{"WinstonLEFT", new Image[] { Properties.Resources.Winstonleft1, Properties.Resources.WinstonLeft2 }},
			{"WinstonRIGHT", new Image[] { Properties.Resources.WinstonRight1, Properties.Resources.WinstonRight2 }},
			{"ViggoUP", new Image[] { Properties.Resources.ViggoUp1, Properties.Resources.ViggoUp2 }},
			{"ViggoDOWN", new Image[] { Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2 }},
			{"ViggoLEFT", new Image[] { Properties.Resources.ViggoLeft1, Properties.Resources.ViggoLeft2 }},
			{"ViggoRIGHT", new Image[] { Properties.Resources.ViggoRight1, Properties.Resources.ViggoRight2 }},
			{"MarquisUP", new Image[] { Properties.Resources.MarquisUp1, Properties.Resources.MarquisUp2 }},
			{"MarquisDOWN", new Image[] { Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2 }},
			{"MarquisLEFT", new Image[] { Properties.Resources.MarquisLeft1, Properties.Resources.MarquisLeft2 }},
			{"MarquisRIGHT", new Image[] { Properties.Resources.MarquisRight1, Properties.Resources.MarquisRight2 }}
		};
		private readonly Image Wall = Properties.Resources.wall;

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
			int width = maze.GetLength(1) * cellSize;
			int height = maze.GetLength(0) * cellSize;
			Bitmap bmp = new(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				for (int x = 0; x < maze.GetLength(0); x++)
				{
					for (int y = 0; y < maze.GetLength(1); y++)
					{
						var color = maze[x, y] switch
						{
							Cell.SpeedBoost => Color.Yellow,
							Cell.Key => Color.Blue,
							Cell.Empty => Color.White,
							_ => Color.White,
						};
						g.FillRectangle(new SolidBrush(color), y * cellSize, x * cellSize, cellSize, cellSize);

						// Draw ghosts
						if (maze[x, y] == Cell.Cain)
						{
							var currentPos = new CellCoordinates(x, y);
							var direction = GetDirection(CainPrevPos, currentPos);
							Image ghostImage = GetCharacterImage("Cain", direction, ref animationFrame);
							g.DrawImage(ghostImage, y * cellSize, x * cellSize, cellSize, cellSize);
							CainPrevPos = currentPos;
						}
						else if (maze[x, y] == Cell.Winston)
						{
							var currentPos = new CellCoordinates(x, y);
							var direction = GetDirection(WinstonPrevPos, currentPos);
							Image ghostImage = GetCharacterImage("Winston", direction, ref animationFrame);
							g.DrawImage(ghostImage, y * cellSize, x * cellSize, cellSize, cellSize);
							WinstonPrevPos = currentPos;
						}
						else if (maze[x, y] == Cell.Viggo)
						{
							var currentPos = new CellCoordinates(x, y);
							var direction = GetDirection(ViggoPrevPos, currentPos);
							Image ghostImage = GetCharacterImage("Viggo", direction, ref animationFrame);
							g.DrawImage(ghostImage, y * cellSize, x * cellSize, cellSize, cellSize);
							ViggoPrevPos = currentPos;
						}
						else if (maze[x, y] == Cell.Marquis)
						{
							var currentPos = new CellCoordinates(x, y);
							var direction = GetDirection(MarquisPrevPos, currentPos);
							Image ghostImage = GetCharacterImage("Marquis", direction, ref animationFrame);
							g.DrawImage(ghostImage, y * cellSize, x * cellSize, cellSize, cellSize);
							MarquisPrevPos = currentPos;
						}
						else if (maze[x, y] == Cell.John)
						{
							PlayerEndPos = new CellCoordinates(x, y);
						}
						else if (maze[x, y] == Cell.Wall)
							g.DrawImage(Wall, y * cellSize, x * cellSize, cellSize, cellSize);
					}
				}

				// Draw player
				PointF playerPos = InterpolatePosition(PlayerStartPos, PlayerEndPos, interpolationFactor); // Replace with your actual logic
				Image playerImage = GetCharacterImage("Player", currentDirection, ref animationFrame);
				g.DrawImage(playerImage, playerPos.X, playerPos.Y, cellSize, cellSize);
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

		private void TmrGhost_Tick(object sender, EventArgs e)
		{
			gameManager.StepGhosts();
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void TmrPlayer_Tick(object sender, EventArgs e)
		{
			gameManager.StepPlayer(currentDirection);
			PlayerStartPos = PlayerEndPos;
			PlayerEndPos = gameManager.Player.Position;
			interpolationFactor = 0.0f;
		}

		private Image GetCharacterImage(string characterType, Direction direction, ref int animationFrame)
		{
			string key = characterType + direction.ToString();
			if (!characterImages.ContainsKey(key))
				key = characterType + "Stop";

			Image[] images = characterImages[key];
			Image image = images[animationFrame % images.Length];
			animationFrame++;
			return image;
		}

		private void TmrRender_Tick(object sender, EventArgs e)
		{
			if (interpolationFactor < 1.0f)
				interpolationFactor += interpolationStep;

			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private PointF InterpolatePosition(CellCoordinates start, CellCoordinates end, float factor)
		{
			float x = start.col + (end.col - start.col) * factor;
			float y = start.row + (end.row - start.row) * factor;
			return new PointF(x * 20, y * 20);
		}

		private static Direction GetDirection(CellCoordinates src, CellCoordinates dst)
		{
			if (src.col < dst.col)
				return Direction.RIGHT;
			if (src.col > dst.col)
				return Direction.LEFT;
			if (src.row < dst.row)
				return Direction.DOWN;
			if (src.row > dst.row)
				return Direction.UP;
			return Direction.STOP;
		}
	}
}
