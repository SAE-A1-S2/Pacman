using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{

		private readonly GameManager gameManager;
		private Direction currentDirection = Direction.STOP;
		private CellCoordinates CainPrevPos, WinstonPrevPos, ViggoPrevPos, MarquisPrevPos, JohnPrevPos;
		private Dictionary<string, Image[]> characterImages;
		private int animationFrame = 0;
		private readonly Image Wall = Properties.Resources.wall;
		private readonly Image Coin = Properties.Resources.coin;
		private readonly Image Key = Properties.Resources.Key;
		private readonly Image HealthKit = Properties.Resources.MedKit;
		private readonly Image Torch = Properties.Resources.torch;
		private readonly FrmPause frmPause;
		private readonly FrmNotif frmNotif;

		public frmGame(GameMode gameMode)
		{
			// Initialisation de la fenêtre de jeu
			InitializeComponent();
			characterImages = [];
			LoadCharacterImages();
			gameManager = new(gameMode);
			frmPause = new(gameManager);
			frmNotif = new(this);

			lblScore.DataBindings.Add("Text", gameManager.LevelManager, "Score");
			pnlKeys.DataBindings.Add("TabIndex", gameManager.LevelManager, "Key");
			pnlHealth.DataBindings.Add("TabIndex", gameManager.LevelManager.Health, "HealthPoints");
		}

		private void frmGame_Closed(object sender, EventArgs e)
		{
			Application.OpenForms[0]?.Show();
		}

		private void LoadCharacterImages()
		{
			characterImages = new Dictionary<string, Image[]>
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
		}

		private void DrawCharacter(Graphics g, int x, int y, ref CellCoordinates prevPos, string characterType, int cellSize)
		{
			var currentPos = new CellCoordinates(x, y);
			var direction = characterType == "Player" ? currentDirection : GetDirection(prevPos, currentPos);
			Image characterImage = GetCharacterImage(characterType, direction, ref animationFrame);
			g.DrawImage(characterImage, y * cellSize, x * cellSize, cellSize, cellSize);
			prevPos = currentPos;
		}


		public void DisplayMaze(Cell[,] maze)
		{
			int cellSize = 30;
			int width = maze.GetLength(1) * cellSize;
			int height = maze.GetLength(0) * cellSize;
			Bitmap bmp = new(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				for (int x = 0; x < maze.GetLength(0); x++)
				{
					for (int y = 0; y < maze.GetLength(1); y++)
					{
						switch (maze[x, y])
						{
							case Cell.Cain:
								DrawCharacter(g, x, y, ref CainPrevPos, "Cain", cellSize);
								break;
							case Cell.Winston:
								DrawCharacter(g, x, y, ref WinstonPrevPos, "Winston", cellSize);
								break;
							case Cell.Viggo:
								DrawCharacter(g, x, y, ref ViggoPrevPos, "Viggo", cellSize);
								break;
							case Cell.Marquis:
								DrawCharacter(g, x, y, ref MarquisPrevPos, "Marquis", cellSize);
								break;
							case Cell.John:
								DrawCharacter(g, x, y, ref JohnPrevPos, "Player", cellSize);
								break;
							case Cell.Wall:
								g.DrawImage(Wall, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Coin:
								g.DrawImage(Coin, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.HealthKit:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(HealthKit, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Torch:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Torch, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Key:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								g.DrawImage(Key, y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							case Cell.Empty:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
							default:
								g.FillRectangle(new SolidBrush(Color.White), y * cellSize, x * cellSize, cellSize, cellSize);
								break;
						}
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
				currentDirection = direction;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			HandleKeyInput(keyData);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void frmGame_Load(object sender, EventArgs e)
		{
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void TmrPlayer_Tick(object sender, EventArgs e)
		{
			gameManager.StepPlayer(currentDirection);
			gameManager.StepGhosts();
			gameManager.CheckGhostCollisions();
			DisplayMaze(gameManager.LevelManager.LevelMap);
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

		private void btnPause_Click(object sender, EventArgs e)
		{
			gameManager.Pause();
			frmPause.ShowDialog(this);
		}

		private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
		{
			gameManager.Pause();
			frmNotif.ShowDialog(this);
			if (!frmNotif.Result)
			{
				e.Cancel = true;
				gameManager.Resume();
			}
		}

		private void pnlKeys_TabIndexChanged(object sender, EventArgs e)
		{
			if (pnlKeys.TabIndex == 1)
			{
				picKey1.Image = Properties.Resources.Key;
				picKey2.Image = Properties.Resources.noRessources;
			}
			else if (pnlKeys.TabIndex == 2)
			{
				picKey1.Image = Properties.Resources.Key;
				picKey2.Image = Properties.Resources.Key;
			}
			else
			{
				picKey1.Image = Properties.Resources.noRessources;
				picKey2.Image = Properties.Resources.noRessources;
			}

		}

		private void pnlHealth_TabIndexChanged(object sender, EventArgs e)
		{
			if (pnlHealth.TabIndex == 2)
				picHealth.Image = Properties.Resources.fulLife;
			else if (pnlHealth.TabIndex == 1)
				picHealth.Image = Properties.Resources.oneLife;
			else
				picHealth.Image = Properties.Resources.noLife;


			if (gameManager.LevelManager.Health.Lives == 0)
			{
				imgLives1.Image = Properties.Resources.blackHeart;
				imgLives2.Image = Properties.Resources.blackHeart;
				imgLives3.Image = Properties.Resources.blackHeart;
			}
			else if (gameManager.LevelManager.Health.Lives == 1)
			{
				imgLives1.Image = Properties.Resources.redHeart;
				imgLives2.Image = Properties.Resources.blackHeart;
				imgLives3.Image = Properties.Resources.blackHeart;
			}
			else if (gameManager.LevelManager.Health.Lives == 2)
			{
				imgLives1.Image = Properties.Resources.redHeart;
				imgLives2.Image = Properties.Resources.redHeart;
				imgLives3.Image = Properties.Resources.blackHeart;
			}
			else if (gameManager.LevelManager.Health.Lives == 3)
			{
				imgLives1.Image = Properties.Resources.redHeart;
				imgLives2.Image = Properties.Resources.redHeart;
				imgLives3.Image = Properties.Resources.redHeart;
			}
		}
	}
}