using Engine;
using Engine.utils;

namespace PacMan
{
	public partial class frmGame : Form
	{
		public GameManager gameManager { get; private set; }
		private Direction currentDirection = Direction.STOP;
		private CellCoordinates CainPrevPos, WinstonPrevPos, ViggoPrevPos, MarquisPrevPos, JohnPrevPos;
		private Dictionary<string, Image[]> characterImages;
		private int animationFrame = 0;
		private readonly Image Wall = Properties.Resources.wall;
		private readonly Image Coin = Properties.Resources.coin;
		private readonly Image Key = Properties.Resources.Key;
		private readonly Image HealthKit = Properties.Resources.MedKit;
		private readonly Image Torch = Properties.Resources.torch;
		private readonly Image Empty = Properties.Resources.noRessources;
		private readonly FrmPause frmPause;
		private readonly FrmNotif frmNotif;

		public frmGame(GameMode gameMode, string playerName)
		{
			// Initialisation de la fenêtre de jeu
			InitializeComponent();
			characterImages = [];
			gameManager = new GameManager(gameMode, playerName);
			frmPause = new FrmPause(gameManager);
			frmNotif = new FrmNotif(this);
		}

		public frmGame(int sessionId)
		{
			// Initialisation de la fenêtre de jeu
			InitializeComponent();
			characterImages = [];
			gameManager = new GameManager(sessionId);
			frmPause = new FrmPause(gameManager);
			frmNotif = new FrmNotif(this);
		}

		private void frmGame_Load(object sender, EventArgs e)
		{
			UnbindDataSources();
			LoadCharacterImages();
			BindDataSources();
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void UnbindDataSources()
		{
			lblScore.DataBindings.Clear();
			pnlKeys.DataBindings.Clear();
			pnlHealth.DataBindings.Clear();
			PnlBonuses.DataBindings.Clear();
		}

		private void BindDataSources()
		{
			lblScore.DataBindings.Add("Text", gameManager.LevelManager, "Score");
			pnlKeys.DataBindings.Add("TabIndex", gameManager.LevelManager, "Key");
			pnlHealth.DataBindings.Add("TabIndex", gameManager.LevelManager.Health, "HealthPoints");
			PnlBonuses.DataBindings.Add("TabIndex", gameManager.LevelManager.Player.Bonuses, "FrontEndValue");
		}

		public void ReloadForm()
		{
			this.frmGame_Load(this, EventArgs.Empty);
		}

		private void frmGame_Closed(object sender, EventArgs e)
		{
			Application.OpenForms[0]?.Show();
		}

		private void LoadCharacterImages()
		{
			characterImages = new Dictionary<string, Image[]>
			{
				{"PlayerUP", [Properties.Resources.Espion_LookUpWalk_Frame1, Properties.Resources.Espion_LookUpWalk_Frame2]},
				{"PlayerDOWN", [Properties.Resources.Espion_LookDownWalk_Frame1, Properties.Resources.Espion_LookDownWalk_Frame2]},
				{"PlayerLEFT", [Properties.Resources.Espion_LookLeftWalk_Frame1, Properties.Resources.Espion_LookLeftWalk_Frame2]},
				{"PlayerRIGHT", [Properties.Resources.Espion_LookRightWalk_Frame1, Properties.Resources.Espion_LookRightWalk_Frame2]},
				{"PlayerStop", [Properties.Resources.Espion_LookDownStop] },
				{"CainSTOP", [Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2] },
				{"WinstonSTOP", [Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2] },
				{"ViggoSTOP", [Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2] },
				{"MarquisSTOP", [Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2] },
				{"CainUP", [Properties.Resources.CainFaceUp1, Properties.Resources.CainFaceUp2] },
				{"CainDOWN", [Properties.Resources.CainFaceDown1, Properties.Resources.CainFaceDown2] },
				{"CainLEFT", [Properties.Resources.CainLeft1, Properties.Resources.CainLeft2] },
				{"CainRIGHT", [Properties.Resources.CainRight1, Properties.Resources.CainRight2] },
				{"WinstonUP", [Properties.Resources.WinstonUp1, Properties.Resources.WinstonUp2] },
				{"WinstonDOWN", [Properties.Resources.WinstonDown1, Properties.Resources.WinstonDown2] },
				{"WinstonLEFT", [Properties.Resources.Winstonleft1, Properties.Resources.WinstonLeft2] },
				{"WinstonRIGHT", [Properties.Resources.WinstonRight1, Properties.Resources.WinstonRight2] },
				{"ViggoUP", [Properties.Resources.ViggoUp1, Properties.Resources.ViggoUp2] },
				{"ViggoDOWN", [Properties.Resources.ViggoDown1, Properties.Resources.ViggoDown2] },
				{"ViggoLEFT", [Properties.Resources.ViggoLeft1, Properties.Resources.ViggoLeft2] },
				{"ViggoRIGHT", [Properties.Resources.ViggoRight1, Properties.Resources.ViggoRight2] },
				{"MarquisUP", [Properties.Resources.MarquisUp1, Properties.Resources.MarquisUp2] },
				{"MarquisDOWN", [Properties.Resources.MarquisDown1, Properties.Resources.MarquisDown2] },
				{"MarquisLEFT", [Properties.Resources.MarquisLeft1, Properties.Resources.MarquisLeft2] },
				{"MarquisRIGHT", [Properties.Resources.MarquisRight1, Properties.Resources.MarquisRight2] }
			};
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
			if (keyData == Keys.P)
			{
				btnPause_Click(this, EventArgs.Empty);
				return true;
			}
			HandleKeyInput(keyData);
			return base.ProcessCmdKey(ref msg, keyData);
		}



		private void TmrPlayer_Tick(object sender, EventArgs e)
		{
			gameManager.StepPlayer(currentDirection);
			gameManager.StepGhosts();
			if (gameManager.CheckGhostCollisions())
				currentDirection = Direction.STOP;
			DisplayMaze(gameManager.LevelManager.LevelMap);
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			gameManager.Pause();
			frmPause.ShowDialog(this);
		}

		private void frmGame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.ApplicationExitCall) return;

			gameManager.Pause();
			frmNotif.ShowDialog(this);
			if (frmNotif.Result) return;
			e.Cancel = true;
			gameManager.Resume();
		}

		private void pnlKeys_TabIndexChanged(object sender, EventArgs e)
		{
			(picKey1.Image, picKey2.Image) = pnlKeys.TabIndex switch
			{
				1 => (Key, Empty),
				2 => (Key, Key),
				_ => (Empty, Empty)  // Default
			};
		}


		private void pnlHealth_TabIndexChanged(object sender, EventArgs e) // optimize
		{
			picHealth.Image = pnlHealth.TabIndex switch
			{
				2 => Properties.Resources.fulLife,
				1 => Properties.Resources.oneLife,
				_ => Properties.Resources.noLife
			};

			int lives = gameManager.LevelManager.Health.Lives;
			imgLives1.Image = lives > 0 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
			imgLives2.Image = lives > 1 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
			imgLives3.Image = lives > 2 ? Properties.Resources.redHeart : Properties.Resources.blackHeart;
		}


		private void PnlBonuses_TabIndexChanged(object sender, EventArgs e)
		{
			(PicBonus1.Image, PicBonus2.Image) = PnlBonuses.TabIndex switch
			{
				0 => (Empty, Empty),
				10 => (HealthKit, Empty),
				20 => (Torch, Empty),
				12 => (HealthKit, Torch),
				21 => (Torch, HealthKit),
				_ => (Empty, Empty)
			};
		}
	}
}